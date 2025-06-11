# ATEM Switcher Control

โปรแกรมควบคุม ATEM Switcher ผ่าน BMDSwitcherAPI

## หน้าตาโปรแกรม

```
+------------------------------------------+
|  IP Address: [192.168.1.1]  [เชื่อมต่อ]   |
|                                          |
|  Program: 1 - Camera 1                   |
|  Preview: 2 - Camera 2                   |
|                                          |
|  [Camera 1 ▼]        [เลือก Input]       |
|                                          |
|  [Cut]  [Auto]                          |
|                                          |
|  สถานะ: เชื่อมต่อสำเร็จ                  |
+------------------------------------------+
```

## คุณสมบัติ

- เชื่อมต่อกับ ATEM Switcher ผ่าน IP Address
  - บันทึก IP Address อัตโนมัติ
  - แสดงสถานะการเชื่อมต่อ
- แสดงสถานะ Program และ Preview Input
  - แสดงหมายเลข Input
  - แสดงชื่อ Input
- เปลี่ยน Input ผ่าน ComboBox
  - แสดงรายการ Input ทั้งหมด
  - เลือก Input ได้ง่าย
- ทำ Cut และ Auto Transition
  - Cut: เปลี่ยนภาพทันที
  - Auto: เปลี่ยนภาพแบบอัตโนมัติ

## การติดตั้ง

1. ดาวน์โหลดไฟล์ติดตั้งล่าสุดจาก [Releases](https://github.com/poteto22/ATEM-Switcher-Control/releases)
2. เปิดไฟล์ `ATEM-Switcher-Control-Setup.exe`
3. ทำตามขั้นตอนการติดตั้ง

## การใช้งาน

1. เปิดโปรแกรม ATEM Switcher Control
2. ใส่ IP Address ของ ATEM Switcher
3. กดปุ่ม Connect
4. เลือก Input ที่ต้องการจาก ComboBox
5. กดปุ่ม Take เพื่อสลับภาพ

## การพัฒนา

### ความต้องการของระบบ
- .NET 6.0 SDK
- Visual Studio 2022 หรือ Rider
- Blackmagic Desktop Video SDK

### การ Build
1. Clone repository
```bash
git clone https://github.com/poteto22/ATEM-Switcher-Control.git
```

2. Build โปรเจค
```bash
dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true /p:IncludeNativeLibrariesForSelfExtract=true
```

3. สร้าง installer
- เปิดไฟล์ `installer.iss` ใน Inno Setup
- กดปุ่ม Build > Compile

## หมายเหตุ
- ไฟล์ `BMDSwitcherAPI64.dll` จะถูกติดตั้งโดยอัตโนมัติพร้อมกับโปรแกรม
- หากมีปัญหาในการเชื่อมต่อ ให้ตรวจสอบว่า ATEM Switcher เปิดอยู่และสามารถเข้าถึงได้จากเครือข่าย

## License

MIT License 