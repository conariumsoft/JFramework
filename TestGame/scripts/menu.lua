grab("JFramework.Graphics");
grab("JFramework.Common");

local scene = UIScene({
	--Name = "Scene",

});

local BG = RectNode({
	Size = UICoords.FromScale(0.25, 0.3),
	Position = UICoords.FromPixels(10, 10),
	Color = Color(1, 0.5, 0),
	Parent = scene,

});

local border = Bo

return scene;