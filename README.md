TCAutoBackup
============

Syntax
------------

  -s, --teamCityServerUrl            Required. (Default: http://localhost:81/) 
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

As an alternative to using command line parameters, you can edit `TCAutoBackup.exe.config`.
