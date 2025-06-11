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

1. ติดตั้ง .NET 6.0 SDK หรือใหม่กว่า
2. Clone repository:
```bash
git clone https://github.com/poteto22/ATEM-Switcher-Control.git
```
3. รันโปรแกรม:
```bash
cd ATEM-Switcher-Control
dotnet run
```

## การใช้งาน

1. กรอก IP Address ของ ATEM Switcher
   - ค่าเริ่มต้น: 192.168.1.1
   - ค่าจะถูกบันทึกอัตโนมัติ
2. กดปุ่ม "เชื่อมต่อ"
   - รอจนกว่าจะเชื่อมต่อสำเร็จ
   - รายการ Input จะถูกโหลดมาแสดง
3. เลือก Input จาก ComboBox
   - แสดงหมายเลขและชื่อ Input
   - เลือก Input ที่ต้องการ
4. กดปุ่ม "เลือก Input" เพื่อเปลี่ยน Preview
   - Input ที่เลือกจะไปแสดงที่ Preview
   - Program จะยังคงเหมือนเดิม
5. ใช้ปุ่ม Cut หรือ Auto เพื่อทำ Transition
   - Cut: เปลี่ยนภาพทันที
   - Auto: เปลี่ยนภาพแบบอัตโนมัติ

## การพัฒนา

- Visual Studio 2022 หรือใหม่กว่า
- .NET 6.0 SDK
- BMDSwitcherAPI package

## License

MIT License 