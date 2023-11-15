function _main() -- Runs on first frame
	print("Hello, I run on the first frame of the script!")
end

function _loop() -- Runs every frame
	print("Hello, I run every frame of the script!")

	return _loop() -- _loop is a coroutine so it has to be returned, unlike _main
end

_main()