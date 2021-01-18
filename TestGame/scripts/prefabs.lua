local function GetButton(text, onActivate)
	local ButtonRect = RectNode({
		Color = Color(1,1,1),
		Size = UICoords.FromPixels(200, 80),
		Position = UICoords.FromPixels(300, 300)
	});

	local Text = TextNode({
		Parent = ButtonRect,
		Text = text,
		TextColor = Color(0,0.1,0),
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
	InputListener.OnClicked:Hook(onActivate);

	local prevColor;

	InputListener.OnMouseEnter:Hook(function()
		prevColor = ButtonRect.Color;
		ButtonRect.Color = prevColor * 0.7;
		debug("Enter")
	end);

	InputListener.OnMouseExit:Hook(function()
		ButtonRect.Color = prevColor;
		debug("Exit")
	end);

	InputListener.OnMouseDown:Hook(function()
		ButtonRect.Color = prevColor * 0.4;
		debug("MouseDown")
	end);

	InputListener.OnMouseUp:Hook(function()
		ButtonRect.Color = prevColor;
		debug("MouseUp")
	end);

	return ButtonRect;
end



return {
	new_button = GetButton,
}