# ATEM Switcher Control

โปรแกรมควบคุม ATEM Switcher ผ่าน BMDSwitcherAPI

## คุณสมบัติ

- เชื่อมต่อกับ ATEM Switcher ผ่าน IP Address
- แสดงสถานะ Program และ Preview Input
- เปลี่ยน Input ผ่าน ComboBox
- ทำ Cut และ Auto Transition
- บันทึก IP Address อัตโนมัติ

## การติดตั้ง

1. ติดตั้ง .NET 6.0 SDK หรือใหม่กว่า
2. Clone repository:
```bash
git clone https://github.com/[username]/ATEM-Switcher-Control.git
```
3. รันโปรแกรม:
```bash
cd ATEM-Switcher-Control
dotnet run
```

## การใช้งาน

1. กรอก IP Address ของ ATEM Switcher
2. กดปุ่ม "เชื่อมต่อ"
3. เลือก Input จาก ComboBox
4. กดปุ่ม "เลือก Input" เพื่อเปลี่ยน Preview
5. ใช้ปุ่ม Cut หรือ Auto เพื่อทำ Transition

## การพัฒนา

- Visual Studio 2022 หรือใหม่กว่า
- .NET 6.0 SDK
- BMDSwitcherAPI package

## License

MIT License 