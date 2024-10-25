import pyodbc

connection_string = (
    "DRIVER={ODBC Driver 17 for SQL Server};"
    "SERVER=sqlserver,1434;"
    "DATABASE=EventCheckinManagement1;"
    "UID=sa;"
    "PWD=12345;"
    "TrustServerCertificate=Yes;"
)

try:
    conn = pyodbc.connect(connection_string)
    print("Connection successful!")
except Exception as e:
    print(f"Connection failed: {e}")
