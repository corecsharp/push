@echo off  

echo ��ʼ����nuget 

for /d %%a in (C:\Users\%USERNAME%\.nuget\packages\schubert.*) do (  
  @echo "%%a" 
  rd /s /q "%%a"  
)

echo �������

echo ��ʼ��ԭ
dotnet restore --no-cache
echo ��ԭ�ɹ�
 