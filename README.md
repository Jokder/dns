# DNS server
Kernel code is forked from:https://github.com/kapetan/dns.

﻿#Description

這是一個簡易的dns服務器,可以部署到本機,或者局域網的其他機器.主要是爲了解決中國dns污染以及在解決了dns污染以後還能解析到正確的cdn.主要原理是内置了兩個dns上游,一個isp的dns上游,一個國外的dns上游,一個中國域名的白名單.在白名單的域名將會通過isp的dns上游解析,其他通過國外的例如8.8.8.8來解析.
因爲爲了編譯出來的可執行文件衹有一個,沒有其他dll,因此使用的別人的輪子目前是把別人的輪子的代碼copy到項目中的,代碼還在整理,過些日子會上傳上來.

﻿#Support Platform

Windows上需要安裝.net framework 4.5+
linux上需要安裝mono(我也不曉得需要安裝什麽版本的mono,請執行安裝目前最新的一定可行)


﻿# Usage

請在release裏面下載可執行文件.
### Windows
雙擊magic-dns.exe
### Linux
./magic-dns.exe


﻿# Config
#only ip support
```conf
Port=53	#啓動該dns服務器所使用的端口,默認爲53.
IspDnsServer=114.114.114.114		#一個用來解析白名單的,中國的DNS的上游,默認爲114.114.114.114,推薦使用ISP提供的
OtherDnsServer=8.8.4.4		#其他DNS服務器,默認爲8.8.8.8
GlobalDnsServer=$OtherDnsServer		#全局DNS服務器的上游,可以填寫具體的IP,也可以填寫上面的兩個上游的名字
WhiteListServer=http://chinadomains.info/whitelist/getall		#白名單地址
```


﻿#補充

release裏面的whitelist-tool.zip是一個白名單服務器,你可以自己部署一個,在dns服務器的配置文件中修改白名單地址即可
whitelist-tool可以部署到windows或者linux上,是golang編譯出來的,理論上在絕大多數電腦都能直接運行,不需要安裝什麽依賴庫.whitelist-toole的默認端口是89,具體可以在conf裏面修改配置.
