# 🚭 Smoking Zero Digital Platform

**Smoking Zero Digital** là một nền tảng hỗ trợ cai thuốc lá theo mô hình đăng ký, kết nối người dùng với các huấn luyện viên cá nhân để huấn luyện 1-1 theo lộ trình được cá nhân hóa. Hệ thống đảm bảo trải nghiệm huấn luyện hiệu quả, bảo mật và trực quan.

---

## 📌 Mục lục

- [🎯 Giới thiệu](#-giới-thiệu)
- [📦 Công nghệ sử dụng](#-công-nghệ-sử-dụng)
- [🧠 Kiến trúc & Mẫu thiết kế](#-kiến-trúc--mẫu-thiết-kế)
- [⚙️ Cấu trúc dự án](#️-cấu-trúc-dự-án)
- [🚀 Hướng dẫn chạy dự án](#-hướng-dẫn-chạy-dự-án)
- [🔐 Tính năng chính](#-tính-năng-chính)
- [📬 Liên hệ](#-liên-hệ)

---

## 🎯 Giới thiệu

### 1. Mục đích

Smoking Zero Digital được thiết kế để giúp người hút thuốc lá:
- Có lộ trình huấn luyện bài bản, dễ tiếp cận.
- Nhận được hướng dẫn 1-1 từ các huấn luyện viên chuyên nghiệp.
- Cải thiện sức khỏe và xây dựng lại hạnh phúc gia đình thông qua quá trình cai thuốc.

### 2. Phạm vi

Nền tảng hỗ trợ các tính năng:
- Đăng ký gói huấn luyện.
- Kết nối người dùng và huấn luyện viên qua hệ thống chat.
- Theo dõi tiến trình huấn luyện.
- Quản lý thông tin người dùng, gói đăng ký và thanh toán.
- Gửi email thông báo, xác thực và OTP.

---

## 📦 Công nghệ sử dụng

### 💻 Programming Languages

- **C#** – Logic backend và API.
- **HTML/CSS/JS** – Giao diện và tương tác người dùng.

### 🧱 Frameworks

- **ASP.NET Core 6.1 – Razor Pages**: Phát triển giao diện render phía server.
- **ASP.NET Web API**: Triển khai các RESTful API cho frontend/backend.
- **ASP.NET Identity**: Quản lý đăng nhập, phân quyền người dùng.

### 🧩 Third-Party Services

- **VNPay Sandbox** – Tích hợp thanh toán online.
- **SMTP (Gmail, Outlook,...)** – Gửi email xác thực và thông báo.
- **Google Cloud OAuth2** – Đăng nhập với tài khoản Google.

---

## 🧠 Kiến trúc & Mẫu thiết kế

### 📐 Kiến trúc

- **Clean Architecture**: Tách biệt các tầng `Domain`, `Application`, `Infrastructure`, và `Presentation`.

### 🧰 Design Patterns

- **CQRS**
- **Mediator**
- **Repository**
- **Unit Of Work**
- **Dependency Injection**
- **DTO**
- **Factory**
- **Builder**
- **Strategy**
- **Observer**

---

2. Cấu hình database
Tạo SQL Server database SmokingZeroDB

Cập nhật chuỗi kết nối trong appsettings.json

3. Apply migration
bash
cd SmokeZeroDigitalAPI
dotnet ef database update
4. Chạy dự án
bash
dotnet run --project SmokeZeroDigitalProject
5. Truy cập giao diện
Mở trình duyệt và vào địa chỉ: https://localhost:7146

🔐 Tính năng chính
 Đăng ký / đăng nhập bằng Email hoặc Google OAuth

 Quản lý gói huấn luyện và thanh toán VNPay

 Chat realtime giữa huấn luyện viên và người dùng (SignalR)

 Ghi nhận tiến trình cai thuốc

 Gửi email xác thực, OTP và thông báo

 Phân quyền User/Admin/Coach

 UI Razor kết hợp API backend rõ ràng

"Together we fight addiction, rebuild health, and empower families." 🚭
