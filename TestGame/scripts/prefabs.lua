local function GetButton(text, onActivate)
	local ButtonRect = RectNode({
		Color = Color(1,1,1),
		Size = UICoords.FromPixels(200, 30),
		Position = UICoords.FromPixels(300, 300)
	});

	local Text = TextNode({
		Parent = ButtonRect,
		Text = text,
		Color = Color(0,0,0),
		XAlignment = TextXAlignment.Center,
		YAlignment = TextYAlignment.Center,
	});

	local Border = OutlineRectBorderNode({
		Parent = ButtonRect,
		Color = Color(0,0,0),
		Thickness = 4,
	});

	local InputListener = ButtonInputListenerNode({
		Parent = ButtonRect,
	})
	InputListener.OnClicked = onActivate;

	return ButtonRect;
end



return {
	new_button = GetButton,
}