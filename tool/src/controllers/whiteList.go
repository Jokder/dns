package controllers

import (
	_"encoding/json"
	"github.com/astaxie/beego"
	_ "os/user"
	_"io/ioutil"
	_"os/user"
	"fmt"
	"os"
	"bufio"
	"encoding/json"
	"util"
	"github.com/ahmetb/go-linq"
)

type WhiteListController struct {
	beego.Controller
}

func (this *WhiteListController) GetALl() {
	fileContent, _ := getFileLines("public/white-list.txt")
	this.Data["json"] = fileContent
	this.ServeJSON()
}

func (this *WhiteListController) Post() {
	var addList [] string
	json.Unmarshal(this.Ctx.Input.RequestBody, &addList)
	path := "public/white-list.txt";
	domains, err := getFileLines(path)
	if err != nil {
		return
	}
	if len(addList) > 0 {
		file, err := os.OpenFile("public/white-list.txt", os.O_APPEND|os.O_WRONLY, 0600)
		if err != nil {
			fmt.Print(err)
		}
		defer file.Close()
		for _, line := range addList {
			var domain = util.StripURLParts(line)
			if len(domain) <= 0 {
				continue
			}
			var search []string
			linq.From(domains).WhereT(func(x string) bool { return x == domain }).ToSlice(&search)
			if len(search) > 0 {
				continue
			}
			file.WriteString("\n" + domain)
		}
	}
	this.Data["json"] = true
	this.ServeJSON()
}

func getFileLines(path string) ([]string, error) {
	var fileContent []string
	file, err := os.Open(path)
	if err != nil {
		return nil, err
	}
	defer file.Close()
	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		line := scanner.Text()
		fmt.Printf(line)
		fileContent = append(fileContent, line)
	}
	return fileContent, err
}
