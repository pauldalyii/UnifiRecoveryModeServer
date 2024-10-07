# Unifi Recovery Mode Web Server
This repository provides a simple web server to aid in the recovery of Ubiquiti devices by serving firmware files to devices in recovery mode.

Unifi's [recovery mode instructions](https://help.ui.com/hc/en-us/articles/360043360253-UniFi-Recovery-Mode) suggest leveraging python.  I made this repo because I already had .NET installed and didn't want to install python on the computer I was using at the time.

## Instructions

1. **Set Network Adapter IP**: Configure your network adapter's IP address to `192.168.1.99` with a subnet mask of `255.255.255.0` (`Get-NetAdapter` and `New-NetIPAddress -InterfaceIndex <Obtain from Get-NetAdapter> -IPAddress 192.168.1.99 -PrefixLength 24`).
2. **Download Firmware**: Download the latest firmware for your device from [Ubiquiti](https://www.ui.com/download/releases/firmware). Rename the firmware file to `fwupdate.bin` and place it in the same directory as `Program.cs`.
3. **Start the Web Server**: Start the web server by pressing `F5` or running `dotnet run` in a terminal.
4. **Configure Firewall**: Ensure there is an inbound firewall rule allowing traffic on port 80. (ex: `New-NetFirewallRule -DisplayName "Allow HTTP" -Direction Inbound -Protocol TCP -LocalPort 80 -Action Allow`)
5. **Connect Device**: Connect an Ethernet cable between the device and your computer.
6. **Enter Recovery Mode**: Put the device into recovery mode by holding down the reset button while powering it on.

## Logging

The server logs each request, including the IP address of the device and the requested path. This information, along with the LED status on the device, helps monitor the recovery process.