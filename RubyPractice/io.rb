$stdout.print "$stdout.print "
$stderr.print "$stdout.print "

require "open-uri"

open("http://www.baidu.com"){|io|
	puts io.read
}