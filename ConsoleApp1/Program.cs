﻿using System.Text;

string token = "CfDJ8HWopNUJ0w9Ool8_5dATdOpuKYqcss0nkRY-oDEFIeX5Tms9YZARNDjeQtUPNXSUA-bbQECCmIfcKNanDIxebCC0DwFLCbB7iG-ZfBLR5BWbx3r1Jbgmx1IsXC_EJZHeMxhK1aWe3XHVZ-scKqx6fI2STiLQ-hepVTQyLtPgYyD1Vs7ojTiNqbHiC77_E3dixd9TARUNd3frtkhVkNq3XdaDPaDOd_qRNTnO4SUA2eS_hWdSy2Y3XRwyess7AYk9JvMIIWP6ITqaC0WKpEMMPSdYXKju0kB6Wkg5DxSlTQSCSK6HGbZvf8J0aWsfjmZ6X2XHdtEpHLAvCw-Bpi3wHcObnDW8Sfg_i0xnVX4dMjqftoqcFqgY0jrehZbxOsFIzpRfLxpzgE-Xu6lF7e7aUSwXgJlLem4h18aLf_n1x1NVWXpDQqZAX1arZSWHaug6y5_g3t_5BSMqdW4tIa0uuqCXYkJj7STDo1lgfAUmmaMO8PAqxc0-w5hhivuJx0EAOYvXMVQjxeggFM00I0eDiIl1Pubf2lQWzj5mGc9xUxmyQTAW6jiT7970Ke36Z40FsUCzvUIXuEo-P7lIoPrZif8tVTLfjiFS1_wiY2U_nzjBQWrU9H_1ZksjmqBMKUF2LrIoHAymNv6m5WZXwYVHB6oEvB54M57ghJsOk7DFvDjL09h-0FxsmmWGmERgSzGX1Q";
var parts = token.Split('.');

var header = Encoding.UTF8.GetString(Convert.FromBase64String(parts[0]));
var payload = Encoding.UTF8.GetString(Convert.FromBase64String(parts[1]));

Console.WriteLine($"Header: {header}");
Console.WriteLine($"Payload: {payload}");

