# ĐỒ ÁN .NET CỦA Võ Thuận LỚP DH19PM
## CÁC FRAMEWORK SỬ DỤNG 
..1. Entity Framework core
..2. Material Design
---
## NỀN TẢNG .NET 5 (ngựa ngựa dùng .net 5 thử cho biết :v)
---
## CÁCH CÀI ĐẶT
* kết nối cơ sở dữ liệu
---
..* Thay đôi chuỗi kết nối tại file [appseting.jon] và [App.xaml.cs] theo định dạng
...- Đối với đăng nhập không cần tài khoảng và mật khẩu:
... "Server=[tên cơ sở dữ liệu];Database=[tên dabase];Trusted_Connection=true;"
...- Đối với đăng nhập cần tài khoảng và mật khẩu:
... Server=[tên cơ sở dữ liệu];Database=[tên dabase];User Id=[tài khoản];password=[mật khẩu];Trusted_Connection=False;MultipleActiveResultSets=true;"
..* Tạo cơ sở dữ liệu
... Tools > NutGet Packege Manager > Packege Manager Console
.... Gõ lệnh:  update-database