-- Getting Input

function _loop()
	-- Mouse
	local mousePosition = InputManager:GetMouse().position;
	local leftMouseButton = InputManager:GetMouse().leftButtonPressed; -- works with right too

	-- Print mouse position if holding left mouse button
	if leftMouseButton then
		print(mousePosition)
	end

	local holdingSpace = InputManager:GetKeyboard().spaceKey -- works with every other button too, for exmaple: aKey, qKey, homeKey, deleteKey...

	if holdingSpace then
		print("Player is holding Space") -- Prints simple message
	end


	-- this WILL ERROR YOU if you're not inside your headset, you have to be physically inside vr for this to work
	local holdingRightGrip = InputManager:GetControllerInput().isHoldingRightGrip
	local rightGripValue = InputManager:GetControllerInput().rightGripValue

	if holdingRightGrip then
		print("Right Grip Value: " + rightGripValue) -- PRints simple message
	end

	return _loop()
end