USE [VuVanHoa_nenmong]
GO
/****** Object:  Table [dbo].[tbl_betong]    Script Date: 2021-09-02 16:16:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_betong](
	[id_betong] [int] NOT NULL,
	[mabetong] [varchar](35) NULL,
	[tenbetong] [varchar](35) NULL,
	[rb] [float] NULL,
	[rbt] [float] NULL,
	[eb] [float] NULL,
 CONSTRAINT [PK_tbl_betong] PRIMARY KEY CLUSTERED 
(
	[id_betong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_coc]    Script Date: 2021-09-02 16:16:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_coc](
	[id_coc] [int] IDENTITY(1,1) NOT NULL,
	[MaCoc] [varchar](20) NOT NULL,
	[TenCoc] [nvarchar](35) NULL,
	[DoDai] [nchar](10) NULL,
	[DuongKinhCoc] [float] NULL,
	[SoThanhThep] [tinyint] NULL,
	[DuongKinhThep] [tinyint] NULL,
	[IDCotThep] [int] NULL,
	[id_betong] [int] NULL,
	[SucChiuTai] [float] NULL,
	[is_deleted] [int] NULL,
	[ChuVi_TD] [float] NULL,
	[DienTich_TD] [float] NULL,
 CONSTRAINT [PK_tbl_coc] PRIMARY KEY CLUSTERED 
(
	[MaCoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_cot_thep]    Script Date: 2021-09-02 16:16:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_cot_thep](
	[IDCotThep] [int] NOT NULL,
	[MaCotThep] [varchar](35) NULL,
	[TenCotThep] [varchar](35) NULL,
	[Rs] [float] NULL,
	[Rsc] [float] NULL,
	[Es] [float] NULL,
 CONSTRAINT [PK_tbl_cot_thep] PRIMARY KEY CLUSTERED 
(
	[IDCotThep] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_daimong]    Script Date: 2021-09-02 16:16:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_daimong](
	[Ldai] [float] NULL,
	[Bdai] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_lop_dat]    Script Date: 2021-09-02 16:16:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_lop_dat](
	[id_lop_dat] [int] IDENTITY(1,1) NOT NULL,
	[MaLopDat] [varchar](20) NOT NULL,
	[chieuday] [float] NULL,
	[n] [tinyint] NULL,
	[loaidat] [nvarchar](50) NULL,
	[trangthai] [nvarchar](50) NULL,
	[dungtrongrieng] [float] NULL,
	[modundanhoi] [int] NULL,
	[gocmasat] [int] NULL,
	[doroi] [int] NULL,
	[suckhangxuyen] [int] NULL,
	[is_delete] [int] NULL,
	[k] [float] NULL,
	[alpha] [float] NULL,
	[vitritrentrudiachat] [int] NULL,
 CONSTRAINT [PK_tbl_lop_dat_1] PRIMARY KEY CLUSTERED 
(
	[MaLopDat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_User]    Script Date: 2021-09-02 16:16:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_User](
	[MaNguoiDung] [varchar](20) NOT NULL,
	[Ten] [nvarchar](250) NULL,
	[SoDienThoai] [varchar](25) NULL,
	[MaCoc] [varchar](20) NULL,
	[MaDai] [varchar](35) NULL,
	[Spt] [float] NULL,
	[Cpt] [float] NULL,
	[DiaChat] [varchar](50) NULL,
	[No] [float] NULL,
	[Mo] [float] NULL,
	[Qo] [float] NULL,
	[b_cot] [float] NULL,
	[h_cot] [float] NULL,
	[h_dai] [float] NULL,
	[Bdai] [float] NULL,
	[Ldai] [float] NULL,
 CONSTRAINT [PK_tbl_User] PRIMARY KEY CLUSTERED 
(
	[MaNguoiDung] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tbl_betong] ([id_betong], [mabetong], [tenbetong], [rb], [rbt], [eb]) VALUES (0, N'BT_B15', N'B15', 8.5, 0.75, 0)
INSERT [dbo].[tbl_betong] ([id_betong], [mabetong], [tenbetong], [rb], [rbt], [eb]) VALUES (1, N'BT_B20', N'B20', 11.5, 0.9, 0)
INSERT [dbo].[tbl_betong] ([id_betong], [mabetong], [tenbetong], [rb], [rbt], [eb]) VALUES (2, N'BT_B25', N'B25', 14.5, 1.05, 0)
INSERT [dbo].[tbl_betong] ([id_betong], [mabetong], [tenbetong], [rb], [rbt], [eb]) VALUES (3, N'BT_B30', N'B30', 17, 1.2, 0)
INSERT [dbo].[tbl_betong] ([id_betong], [mabetong], [tenbetong], [rb], [rbt], [eb]) VALUES (4, N'BT_B35', N'B35', 19.5, 1.3, 0)
INSERT [dbo].[tbl_betong] ([id_betong], [mabetong], [tenbetong], [rb], [rbt], [eb]) VALUES (5, N'BT_B40', N'B40', 22, 1.4, 0)
GO
SET IDENTITY_INSERT [dbo].[tbl_coc] ON 

INSERT [dbo].[tbl_coc] ([id_coc], [MaCoc], [TenCoc], [DoDai], [DuongKinhCoc], [SoThanhThep], [DuongKinhThep], [IDCotThep], [id_betong], [SucChiuTai], [is_deleted], [ChuVi_TD], [DienTich_TD]) VALUES (19, N'cc0003', N'Cọc Thứ Nhất', N'16        ', 0.35, 4, 1, 1, 1, 1533, 0, 1.4, 0.12249999999999998)
INSERT [dbo].[tbl_coc] ([id_coc], [MaCoc], [TenCoc], [DoDai], [DuongKinhCoc], [SoThanhThep], [DuongKinhThep], [IDCotThep], [id_betong], [SucChiuTai], [is_deleted], [ChuVi_TD], [DienTich_TD]) VALUES (10, N'CC001', N'Cọc 20x20', N'16        ', 0.2, 4, 1, 1, 1, 602, 0, 0.8, 0.04)
INSERT [dbo].[tbl_coc] ([id_coc], [MaCoc], [TenCoc], [DoDai], [DuongKinhCoc], [SoThanhThep], [DuongKinhThep], [IDCotThep], [id_betong], [SucChiuTai], [is_deleted], [ChuVi_TD], [DienTich_TD]) VALUES (12, N'CC002', N'Cọc 30x30', N'16        ', 0.3, 8, 1, 0, 1, 1236, 0, 1.2, 0.09)
INSERT [dbo].[tbl_coc] ([id_coc], [MaCoc], [TenCoc], [DoDai], [DuongKinhCoc], [SoThanhThep], [DuongKinhThep], [IDCotThep], [id_betong], [SucChiuTai], [is_deleted], [ChuVi_TD], [DienTich_TD]) VALUES (7, N'COC001', N'Cọc Thứ Nhất', N'16        ', 0.35, 4, 1, 1, 1, 1456, 0, 1.4, 0.1225)
INSERT [dbo].[tbl_coc] ([id_coc], [MaCoc], [TenCoc], [DoDai], [DuongKinhCoc], [SoThanhThep], [DuongKinhThep], [IDCotThep], [id_betong], [SucChiuTai], [is_deleted], [ChuVi_TD], [DienTich_TD]) VALUES (2015, N'COC001123', N'Cọc của Hòa', N'16        ', 0.3, 4, 1, 1, 1, 1120, 0, 1.2, 0.09)
INSERT [dbo].[tbl_coc] ([id_coc], [MaCoc], [TenCoc], [DoDai], [DuongKinhCoc], [SoThanhThep], [DuongKinhThep], [IDCotThep], [id_betong], [SucChiuTai], [is_deleted], [ChuVi_TD], [DienTich_TD]) VALUES (8, N'COC002', N'Cọc Thứ Nhất', N'15        ', 0.3, 4, 1, 1, 1, 1120, 0, 1.2, 0.09)
INSERT [dbo].[tbl_coc] ([id_coc], [MaCoc], [TenCoc], [DoDai], [DuongKinhCoc], [SoThanhThep], [DuongKinhThep], [IDCotThep], [id_betong], [SucChiuTai], [is_deleted], [ChuVi_TD], [DienTich_TD]) VALUES (6, N'COC004', N'Cọc Thứ Nhất', N'16        ', 0.25, 6, 1, 1, 1, 929, 0, 1, 0.0625)
INSERT [dbo].[tbl_coc] ([id_coc], [MaCoc], [TenCoc], [DoDai], [DuongKinhCoc], [SoThanhThep], [DuongKinhThep], [IDCotThep], [id_betong], [SucChiuTai], [is_deleted], [ChuVi_TD], [DienTich_TD]) VALUES (14, N'COC005', N'Cọc Thứ Nhất', N'12.2      ', 0.25, 4, 1, 1, 3, 1144, 0, 1, 0.0625)
INSERT [dbo].[tbl_coc] ([id_coc], [MaCoc], [TenCoc], [DoDai], [DuongKinhCoc], [SoThanhThep], [DuongKinhThep], [IDCotThep], [id_betong], [SucChiuTai], [is_deleted], [ChuVi_TD], [DienTich_TD]) VALUES (15, N'COC010', N'cọc thủ nghiêm 1', N'16        ', 0.32, 4, 1, 0, 1, 1252, 0, 1.28, 0.1024)
INSERT [dbo].[tbl_coc] ([id_coc], [MaCoc], [TenCoc], [DoDai], [DuongKinhCoc], [SoThanhThep], [DuongKinhThep], [IDCotThep], [id_betong], [SucChiuTai], [is_deleted], [ChuVi_TD], [DienTich_TD]) VALUES (1015, N'Coccc123', N'Cọc TO', N'16        ', 0.5, 4, 1, 1, 1, 2776, 0, 2, 0.25)
INSERT [dbo].[tbl_coc] ([id_coc], [MaCoc], [TenCoc], [DoDai], [DuongKinhCoc], [SoThanhThep], [DuongKinhThep], [IDCotThep], [id_betong], [SucChiuTai], [is_deleted], [ChuVi_TD], [DienTich_TD]) VALUES (16, N'TEST', N'TEST', N'3         ', 0.35, 4, 1, 1, 1, 1456, 0, 1.4, 0.12249999999999998)
SET IDENTITY_INSERT [dbo].[tbl_coc] OFF
GO
INSERT [dbo].[tbl_cot_thep] ([IDCotThep], [MaCotThep], [TenCotThep], [Rs], [Rsc], [Es]) VALUES (0, N'CB240T', N'CB240-T', 210, 210, 200000)
INSERT [dbo].[tbl_cot_thep] ([IDCotThep], [MaCotThep], [TenCotThep], [Rs], [Rsc], [Es]) VALUES (1, N'CB300V', N'CB300-V', 260, 260, 200000)
GO
SET IDENTITY_INSERT [dbo].[tbl_lop_dat] ON 

INSERT [dbo].[tbl_lop_dat] ([id_lop_dat], [MaLopDat], [chieuday], [n], [loaidat], [trangthai], [dungtrongrieng], [modundanhoi], [gocmasat], [doroi], [suckhangxuyen], [is_delete], [k], [alpha], [vitritrentrudiachat]) VALUES (6, N'LD001', 4, 12, N'Sét Pha', N'Dẻo mềm', 1.89, 4800, 4800, 4800, 1500, 0, 0.5, 30, 1)
INSERT [dbo].[tbl_lop_dat] ([id_lop_dat], [MaLopDat], [chieuday], [n], [loaidat], [trangthai], [dungtrongrieng], [modundanhoi], [gocmasat], [doroi], [suckhangxuyen], [is_delete], [k], [alpha], [vitritrentrudiachat]) VALUES (8, N'LD002', 3.5, 7, N'Cát Pha', N'Nhão', 1.73, 1050, 1050, 1050, 400, 0, 0.5, 30, 2)
INSERT [dbo].[tbl_lop_dat] ([id_lop_dat], [MaLopDat], [chieuday], [n], [loaidat], [trangthai], [dungtrongrieng], [modundanhoi], [gocmasat], [doroi], [suckhangxuyen], [is_delete], [k], [alpha], [vitritrentrudiachat]) VALUES (10, N'LD003', 5, 23, N'Cát', N'Chảy', 1.86, 15000, 15000, 15000, 4500, 0, 0.5, 100, 3)
INSERT [dbo].[tbl_lop_dat] ([id_lop_dat], [MaLopDat], [chieuday], [n], [loaidat], [trangthai], [dungtrongrieng], [modundanhoi], [gocmasat], [doroi], [suckhangxuyen], [is_delete], [k], [alpha], [vitritrentrudiachat]) VALUES (1011, N'LD004', 90, 40, N'Cuội', N'chặt', 1.96, 24000, 36, 0, 12000, 0, 0.5, 150, 4)
SET IDENTITY_INSERT [dbo].[tbl_lop_dat] OFF
GO
INSERT [dbo].[tbl_User] ([MaNguoiDung], [Ten], [SoDienThoai], [MaCoc], [MaDai], [Spt], [Cpt], [DiaChat], [No], [Mo], [Qo], [b_cot], [h_cot], [h_dai], [Bdai], [Ldai]) VALUES (N'USER001', N'Vũ Văn Hòa', N'0968872539', N'cc0003', NULL, 985.133, 914.66666666666663, NULL, 250.16, 15.9, 3.2, 0.35, 0.45, 0.8, 1.8, 1.8)
INSERT [dbo].[tbl_User] ([MaNguoiDung], [Ten], [SoDienThoai], [MaCoc], [MaDai], [Spt], [Cpt], [DiaChat], [No], [Mo], [Qo], [b_cot], [h_cot], [h_dai], [Bdai], [Ldai]) VALUES (N'USER002', N'Vũ Văn Hòa.', N'0968872539', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tbl_User] ([MaNguoiDung], [Ten], [SoDienThoai], [MaCoc], [MaDai], [Spt], [Cpt], [DiaChat], [No], [Mo], [Qo], [b_cot], [h_cot], [h_dai], [Bdai], [Ldai]) VALUES (N'USER003', N'Nguyễn Văn Hà', N'0968872539', N'CC001', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tbl_User] ([MaNguoiDung], [Ten], [SoDienThoai], [MaCoc], [MaDai], [Spt], [Cpt], [DiaChat], [No], [Mo], [Qo], [b_cot], [h_cot], [h_dai], [Bdai], [Ldai]) VALUES (N'user006', N'Nguyễn Văn A', N'123456', N'cc0003', NULL, 980.467, 914.66666666666663, NULL, 400, 30, 3.2, 30, 40, NULL, NULL, NULL)
GO
ALTER TABLE [dbo].[tbl_coc]  WITH CHECK ADD  CONSTRAINT [FK_tbl_coc_tbl_betong] FOREIGN KEY([id_betong])
REFERENCES [dbo].[tbl_betong] ([id_betong])
GO
ALTER TABLE [dbo].[tbl_coc] CHECK CONSTRAINT [FK_tbl_coc_tbl_betong]
GO
ALTER TABLE [dbo].[tbl_coc]  WITH CHECK ADD  CONSTRAINT [FK_tbl_coc_tbl_cot_thep] FOREIGN KEY([IDCotThep])
REFERENCES [dbo].[tbl_cot_thep] ([IDCotThep])
GO
ALTER TABLE [dbo].[tbl_coc] CHECK CONSTRAINT [FK_tbl_coc_tbl_cot_thep]
GO
ALTER TABLE [dbo].[tbl_User]  WITH CHECK ADD  CONSTRAINT [FK_tbl_User_tbl_coc] FOREIGN KEY([MaCoc])
REFERENCES [dbo].[tbl_coc] ([MaCoc])
GO
ALTER TABLE [dbo].[tbl_User] CHECK CONSTRAINT [FK_tbl_User_tbl_coc]
GO
