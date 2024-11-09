using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.CreateDTO;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using EventCheckinSystem.Services.Interfaces;
using ExcelDataReader;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ExcelDataReader;
using EventCheckinSystem.Repo.DTOs.Paging;
using EventCheckinSystem.Repo.DTOs.ResponseDTO;

namespace EventCheckinSystem.Services.Services
{
    public class GuestServices : IGuestServices
    {
        private readonly IGuestRepo _guestRepo;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly ITimeService _timeService;
        private readonly IValidator<GuestDTO> _guestValidator;
        private readonly IGuestImageRepo _guestImageRepo;
        private readonly IAzureBlobService _azureBlobService;

        public GuestServices(IGuestRepo guestRepo, IMapper mapper, IUserContextService userContextService, ITimeService timeService, IValidator<GuestDTO> guestValidator, IGuestImageRepo guestImageRepo, IAzureBlobService azureBlobService)
        {
            _guestRepo = guestRepo;
            _mapper = mapper;
            _userContextService = userContextService;
            _timeService = timeService;
            _guestValidator = guestValidator;
            _guestImageRepo = guestImageRepo;
            _azureBlobService = azureBlobService;
        }

        public async Task<List<GuestResponse>> GetAllGuestsAsync()
        {
            var guests = await _guestRepo.GetAllGuestsAsync();
            return _mapper.Map<List<GuestResponse>>(guests);
        }

        public async Task<GuestResponse> GetGuestByIdAsync(int guestId)
        {
            var guest = await _guestRepo.GetGuestByIdAsync(guestId);
            return guest != null ? _mapper.Map<GuestResponse>(guest) : null;
        }
        public async Task<List<GuestResponse>> GetGuestByNameAsync(string guestName)
        {
            var guest = await _guestRepo.GetGuestsByNameAsync(guestName);
            return guest != null ? _mapper.Map<List<GuestResponse>>(guest) : null;
        }

        public async Task<GuestDTO> AddGuestAsync(CreateGuestDTO guestDto, IFormFile imageFile)
        {
            try
            {
                if (imageFile != null)
                {
                    guestDto.ImageUrl = await _azureBlobService.UploadImageAsync(imageFile);
                }

                var newGuest = new Guest
                {
                    GuestGroupID = guestDto.GuestGroupID,
                    Name = guestDto.Name,
                    Email = guestDto.Email,
                    PhoneNumber = guestDto.PhoneNumber,
                    Address = guestDto.Address,
                    Birthday = guestDto.Birthday,
                    CreatedBy = _userContextService.GetCurrentUserId(),
                    LastUpdatedBy = _userContextService.GetCurrentUserId(),
                    CreatedTime = _timeService.SystemTimeNow,
                    LastUpdatedTime = _timeService.SystemTimeNow,
                };
                var createdGuest = await _guestRepo.AddGuestAsync(newGuest);
                var guestImage = new GuestImage
                {
                    GuestID = createdGuest.GuestID, 
                    ImageURL = guestDto.ImageUrl,
                    CreatedBy = _userContextService.GetCurrentUserId(),
                    LastUpdatedBy = _userContextService.GetCurrentUserId(),
                    CreatedTime = DateTime.UtcNow,
                    LastUpdatedTime = DateTime.UtcNow
                };
                await _guestImageRepo.CreateGuestImageAsync(guestImage);

                return new GuestDTO
                {
                    GuestID = createdGuest.GuestID,
                    Name = createdGuest.Name,
                    Email = createdGuest.Email,
                    
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the guest", ex);
            }
        }

        public async Task<bool> UpdateGuestAsync(GuestDTO guestDto, IFormFile imageFile = null)
        {
            try
            {
                var existingGuest = await _guestRepo.GetGuestByIdAsync(guestDto.GuestID);
                if (existingGuest == null)
                {
                    throw new Exception("Guest not found");
                }
                if (imageFile != null)
                {
                    guestDto.ImageURL = await _azureBlobService.UploadImageAsync(imageFile);
                    var existingGuestImage = await _guestImageRepo.GetGuestImageByGuestIdAsync(guestDto.GuestID);
                    if (existingGuestImage != null)
                    {
                        existingGuestImage.ImageURL = guestDto.ImageURL;
                        existingGuestImage.LastUpdatedBy = _userContextService.GetCurrentUserId();
                        existingGuestImage.LastUpdatedTime = DateTime.UtcNow;
                        await _guestImageRepo.UpdateGuestImageAsync(existingGuestImage);
                    }
                    else
                    {
                        var newGuestImage = new GuestImage
                        {
                            GuestID = guestDto.GuestID,
                            ImageURL = guestDto.ImageURL,
                            CreatedBy = _userContextService.GetCurrentUserId(),
                            LastUpdatedBy = _userContextService.GetCurrentUserId(),
                            CreatedTime = DateTime.UtcNow,
                            LastUpdatedTime = DateTime.UtcNow
                        };
                        await _guestImageRepo.CreateGuestImageAsync(newGuestImage);
                    }
                }

                _mapper.Map(guestDto, existingGuest);
                existingGuest.LastUpdatedBy = _userContextService.GetCurrentUserId();
                existingGuest.LastUpdatedTime = _timeService.SystemTimeNow;
                return await _guestRepo.UpdateGuestAsync(existingGuest);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the guest", ex);
            }
        }


        public async Task<bool> DeleteGuestAsync(int guestId)
        {
            try
            {
                return await _guestRepo.DeleteGuestAsync(guestId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the guest", ex);
            }
        }

        public async Task<List<GuestResponse>> GetGuestsByGroupIdAsync(int guestGroupId)
        {
            var guests = await _guestRepo.GetGuestsByGroupIdAsync(guestGroupId);
            return _mapper.Map<List<GuestResponse>>(guests);
        }
        public async Task<List<GuestResponse>> GetGuestsByEventIdAsync(int eventId)
        {
            var guests = await _guestRepo.GetGuestsByEventIdAsync(eventId);
            return _mapper.Map<List<GuestResponse>>(guests);
        }

        public async Task<int> ImportGuestsFromExcelAsync(IFormFile file)
        {
            var guests = new List<Guest>();
            var dataTable = ReadExcelFile(file);

            foreach (DataRow row in dataTable.Rows)
            {
                var guestDto = ValidateAndMapRow(row);

                if (guestDto != null)
                {
                    var guestGroupExists = await _guestRepo.GuestGroupExistsAsync(guestDto.GuestGroupID);
                    if (!guestGroupExists)
                    {
                        throw new Exception($"Invalid GuestGroupID: {guestDto.GuestGroupID}. This group does not exist.");
                    }
                    var newGuest = _mapper.Map<Guest>(guestDto);
                    newGuest.IsActive = true;
                    newGuest.IsDelete = false;
                    newGuest.CreatedBy = _userContextService.GetCurrentUserId();
                    newGuest.LastUpdatedBy = newGuest.CreatedBy;
                    newGuest.CreatedTime = _timeService.SystemTimeNow;
                    newGuest.LastUpdatedTime = _timeService.SystemTimeNow;
                    guests.Add(newGuest);
                }
            }
            if (guests.Count == 0)
                return 0;
            try
            {
                await _guestRepo.AddGuestsAsync(guests);
                return guests.Count;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.InnerException?.Message}");
                throw new Exception("An error occurred while saving the guests. Please check the inner exception for details.", ex);
            }
        }


        private GuestDTO ValidateAndMapRow(DataRow row)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(row["Name"].ToString()) ||
                    string.IsNullOrWhiteSpace(row["Email"].ToString()) ||
                    string.IsNullOrWhiteSpace(row["PhoneNumber"].ToString()))
                {
                    return null;
                }

                return new GuestDTO
                {
                    Name = row["Name"].ToString(),
                    Email = row["Email"].ToString(),
                    PhoneNumber = row["PhoneNumber"].ToString(),
                    Address = row["Address"].ToString(),
                    Birthday = DateTime.Parse(row["Birthday"].ToString() ?? DateTime.Now.ToString()),
                    GuestGroupID = int.Parse(row["GuestGroupID"].ToString() ?? "0")
                };
            }
            catch
            {
                return null;
            }
        }

        private DataTable ReadExcelFile(IFormFile file)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using var stream = new MemoryStream();
            file.CopyTo(stream);
            stream.Position = 0;

            using var reader = ExcelReaderFactory.CreateReader(stream);
            var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration
                {
                    UseHeaderRow = true
                }
            });

            return dataSet.Tables[0];
        }

        public async Task<PagedResult<GuestResponse>> GetPagedGuestsAsync(PageRequest pageRequest)
        {
            var pagedGuests = await _guestRepo.GetPagedGuestsAsync(pageRequest);

            return new PagedResult<GuestResponse>
            {
                Items = _mapper.Map<List<GuestResponse>>(pagedGuests.Items),
                TotalCount = pagedGuests.TotalCount,
                PageSize = pagedGuests.PageSize,
                PageNumber = pagedGuests.PageNumber
            };
        }
    }
}

