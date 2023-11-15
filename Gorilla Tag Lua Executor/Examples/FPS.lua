function _loop()
	local fps = 1 / Time.deltaTime -- getting fps by diving one by the time since last frame as a float
	local pos = Rect:New(470, 1, 100, 100) -- x, y, width, height of Rect

	local fpsString = string.format("%.0f", tostring(fps)) -- formats FPS into a string and removes any decimals

	GUI:Label(pos, fpsString) -- displaying the FPS

	return _loop() -- Returning _loop like usual
end