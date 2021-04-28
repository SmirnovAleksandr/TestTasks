echo "AdbScript: " %0 %1 %2
@rem %1 - udid 
@rem %2 - full path to source image
adb.exe -s %1 shell mkdir /sdcard/Download/InstaTest
adb.exe -s %1 push %2  /sdcard/Download/InstaTest/
adb.exe -s %1 shell "am broadcast -a android.intent.action.MEDIA_MOUNTED -d file:///mnt/sdcard/Download"