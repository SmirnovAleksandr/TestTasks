echo "TearDown: " %0 %1 %2
@rem %1 - udid 
@rem %2 - faile name for remove on device 
adb.exe -s %1 shell rm -rf %2
adb.exe -s %1 uninstall com.instagram.android