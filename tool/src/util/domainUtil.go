package util

import "strings"
// stripURLParts removes path, protocol & query from url and returns it.
func StripURLParts(url string) string {
	//Lower case the url
	url = strings.ToLower(url)

	//Strip protocol
	if index := strings.Index(url, "://"); index > -1 {
		url = url[index+3:]
	}

	//Strip path (and query with it)
	if index := strings.Index(url, "/"); index > -1 {
		url = url[:index]
	} else if index := strings.Index(url, "?"); index > -1 { //Strip query if path is not found
		url = url[:index]
	}

	//Return domain
	return url
}
