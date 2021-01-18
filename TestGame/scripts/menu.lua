grab("JFramework.Graphics");
grab("JFramework.Common");

local scene = UIScene({
	--Name = "Scene",

});

local prefabs = require("scripts.prefabs");


local button = prefabs.new_button("Hello world!", function(ev, inputtype)
	debug("IT WORKS!!!");
end)
button.Parent = scene;

local BG = RectNode({
	Size = UICoords.FromScale(0.25, 0.4),
	Position = UICoords.FromPixels(10, 10),
	Color = Color(0.1, 0.1, 0.1),
	Parent = scene,
});

debug("WTF???")
local border = OutlineRectBorderNode({
	Thickness = 2,
	Color = Color(1, 1, 0),
	Parent = BG,
});

local textnode = TextNode({
	--Font = FontManager.Arial,
	Parent = BG,
	TextColor = Color(1,1,1),
	Text = "Testing 123",
})


scene.FocusList = FocusList()
scene.FocusList:Add(BG);
scene.FocusList:Add(button);

return scene;