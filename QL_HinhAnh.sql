-- Yêu cầu:
-- 1. tạo cơ sở dữ liệu QuanLy_HinhAnh
-- 2. tạo bảng Upload_File_HinhAnh

-- 3. path hình ảnh là đường dẫn của hình ảnh trong máy tính

create database QuanLy_HinhAnh;

use QuanLy_HinhAnh;

create table Upload_File_HinhAnh(
	id_HinhAnh int identity(1, 1) primary key,
	path_HinhAnh varchar(500)
);


-- xóa bản ghi có id = 1
delete from Upload_File_HinhAnh
where id_HinhAnh = 1