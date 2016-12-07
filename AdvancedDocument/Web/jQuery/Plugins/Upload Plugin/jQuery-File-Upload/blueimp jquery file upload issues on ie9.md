[blueimp jquery file upload issues on ie9](http://stackoverflow.com/questions/10836870/blueimp-jquery-file-upload-issues-on-ie9)

## question 

having "issues" with the blueimp jquery file uploader, uploads fine, but no progress is reported back to the calling page, only in ie8/ie9.

"done" seems to work properly, its only the "progress" part that isnt working, anyone any ideas?


## answer
	
IE9 does not support XMLHttpRequest level 2, so you won't be able to have progress working with IE < 10. It does not have anything related with the plugin.

http://caniuse.com/xhr2
