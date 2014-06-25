textureHeight=1;

var SetTextureHeight = function(input) 
{
    textureHeight = input;
    return "";
};
SetTextureHeight.filterName = "setTextureHeight";
Library.addFilter("SetTextureHeight");


var MirroredFrameRectY = function(sprite)
{
    return "" + (textureHeight - sprite.frameRect.y - sprite.frameRect.height);
};
MirroredFrameRectY.filterName = "MirroredFrameRectY";
Library.addFilter("MirroredFrameRectY");


var TrimmedPivotX = function(sprite)
{
	var ppX = (sprite.pivotPoint.x - sprite.sourceRect.x) / sprite.sourceRect.width;
    return "" + ppX;
};
TrimmedPivotX.filterName = "TrimmedPivotX";
Library.addFilter("TrimmedPivotX");

var TrimmedMirroredPivotY = function(sprite)
{
	var ppY = (sprite.pivotPoint.y  - sprite.sourceRect.y) / sprite.sourceRect.height;
    return "" + (1 - ppY);
};
TrimmedMirroredPivotY.filterName = "TrimmedMirroredPivotY";
Library.addFilter("TrimmedMirroredPivotY");
