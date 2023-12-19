
- Clone [project](https://github.com/duyhelloworld/webnangcao) về máy
![Github](/.github/images/cloneproject.png)
- Kiểm tra môi trường .NET 7.0

    Mở terminal và chạy lệnh `dotnet --info`
    
    Nếu có báo đỏ ==> Cài đặt .NET 7.0 SDK [tại đây](https://dotnet.microsoft.com/download/dotnet/7.0)

- Kiểm tra môi trường NodeJS
    
    Mở terminal và chạy lệnh `node --version`

    Nếu có báo đỏ ==> Cài đặt NodeJS phiên bản 20.8.1 [tại đây](https://nodejs.org/dist/v20.8.1/node-v20.8.1-x64.msi)

- Mở project bằng Visual Studio Code

- Mở terminal bằng cách nhấn tổ hợp phím `Ctrl + Shift + ~` hoặc `View > Terminal`
![](/.github/images/terminal.png)

- Chạy lệnh `cd ClientApp;` để vào thư mục ClientApp (sau này code FE ở đây tất)

- Chạy lệnh `npm install` để cài đặt các package cần thiết
- Chạy front-end bằng `npm start`

- Chạy lệnh `dotnet restore` để cài đặt các package cần thiết
(Nếu có bug NU1100 thì chạy lệnh `dotnet nuget locals all --clear` và `dotnet nuget sources add -n nuget.org -s https://api.nuget.org/v3/index.json` rồi restore)
- Nếu `dotnet restore` vẫn không tải package thì: 
```
dotnet add package Microsoft.AspNetCore.Authentication.Facebook --version 7.0.12
dotnet add package Microsoft.AspNetCore.Authentication.Google --version 7.0.12
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 7.0.12
dotnet add package Microsoft.AspNetCore.Authentication.OpenIdConnect --version 7.0.12
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 7.0.12
dotnet add package Microsoft.AspNetCore.StaticFiles --version 2.2.0
dotnet add package Microsoft.EntityFrameworkCore --version 7.0.12
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 7.0.12
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 7.0.12
```
- Chạy lệnh `dotnet tool install dotnet-ef` để tải công cụ kết nối db
- Tạo user bằng [User.sql](/Assets/sql/User.sql)
(Mở bằng SQL Server Management Studio và chạy toàn bộ lệnh trong file)
- Chạy lệnh `dotnet ef migrations add InitialDb` để tạo migration
- Chèn data bằng file [Data.sql](/Assets/sql/Data.sql)
- Chạy lệnh `dotnet ef database update` để tạo database

- Chạy project bằng cách `dotnet run` 
![](/.github/images/ketquachaylenh.png)

- Nếu lỗi `Could not obtain ...` thì restart lại SQL Server

> NOTE: Nếu có nhu cầu, có thể down nhạc trên [soundcloud](https://soundcloud.com/) bằng [link](https://vi.savefrom.net/12-cach-tai-nhac-soundcloud-20.html) và thêm vào thư mục [music](/Assets/musics/) 