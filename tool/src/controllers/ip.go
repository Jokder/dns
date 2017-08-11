package controllers

import (
	_"encoding/json"
	"github.com/astaxie/beego"
	_ "os/user"
	_"io/ioutil"
	_"os/user"
	_"strings"
	"strings"
)

type IpController struct {
	beego.Controller
}

func (this *IpController) MyIp() {
	var clientIp ="0.0.0.0"
	ips:=this.Ctx.Input.Proxy()
	if len(ips) > 0 && ips[0] != "" {
		clientIp = ips[0]
	}
	ip := strings.Split(this.Ctx.Request.RemoteAddr, ":")
	if len(ip) > 0 {
		clientIp = ip[0]
	}
	this.Data["json"] = clientIp
	this.ServeJSON()
}