echo "AdbScript: " %0 %1 %2 %3 %4 %5
@rem %1 - udid 
@rem %2 - full path to source image
@rem %3 - full path to Photo Storage On Android ) 
@rem %4 - full path to installation package (apk)
@echo Started: %date% %time%
@rem adb.exe -s %1 install -r -d -g %4 
adb.exe -s %1 shell mkdir %3
adb.exe -s %1 push %2  %3/
@rem adb.exe -s %1 shell "am broadcast -a android.intent.action.MEDIA_SCANNER_SCAN_FILE -d file:///mnt/sdcard/Download/InstaTest/IMG_0651.JPG"
adb.exe -s %1 shell "find /mnt/sdcard/Download/InstaTest/ | while read f; do am broadcast -a android.intent.action.MEDIA_SCANNER_SCAN_FILE -d \"file://${f}\"; done"

@echo Completed: %date% %time%
@rem pause