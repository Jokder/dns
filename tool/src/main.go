package main

import (
	_ "routers"
	"github.com/astaxie/beego"
	_"fmt"
)

func main() {
	if beego.BConfig.RunMode == "dev" {
		beego.BConfig.WebConfig.DirectoryIndex = true
		beego.BConfig.WebConfig.StaticDir["/"] = "public"
	}
	beego.SetStaticPath("/static","public/static")
	beego.Run()
}