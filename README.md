TCAutoBackup
============

Syntax
------------

  -s, --teamCityServerUrl            Required.
                                     TeamCity Server URL

  -u, --authUserName                 Required. 

  -p, --authPassword                 Required. 

  -b, --backupPath                   Required. 

  -h, --numberOfDaysToKeepBackups    (Default: 30) 

  -f, --filename                     

  -t, --addTimestamp                 (Default: True) 

  -c, --includeConfig                (Default: True) 

  -d, --includeDatabase              (Default: True) 

  -l, --includeBuildLogs             (Default: True) 

  -g, --includePersonalChanges       (Default: True) 

  -?, --help                         Display this help screen.

Examples
------------

    TCAutoBackup.exe -s http://localhost:81 -u username -p password -b D:\TeamCity\.BuildServer\backup

As an alternative to using command line parameters, you can edit `TCAutoBackup.exe.config`.
