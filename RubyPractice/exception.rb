file=ARGV[0]

begin
    src=open(file)
	data=src.read
	p data
	src.close
	rescue=>ex
	print ex.message,"\n"
	sleep 1
	#retry
	ensure
	src.close
	print "ensure","\n"
end
