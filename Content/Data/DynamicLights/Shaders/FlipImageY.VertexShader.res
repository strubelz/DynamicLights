﻿<root dataType="Struct" type="Duality.Resources.VertexShader" id="129723834">
  <assetInfo dataType="Struct" type="Duality.Editor.AssetManagement.AssetInfo" id="427169525">
    <customData />
    <importerId dataType="String">BasicShaderAssetImporter</importerId>
    <sourceFileHint dataType="Array" type="System.String[]" id="1100841590">
      <item dataType="String">{Name}.vert</item>
    </sourceFileHint>
  </assetInfo>
  <source dataType="String">void main()
{
	gl_Position = ftransform();
	vec4 newCoord = gl_MultiTexCoord0;
	newCoord.y = 1.0 - newCoord.y;
	gl_TexCoord[0] = newCoord;
	gl_FrontColor = gl_Color;
}</source>
</root>
<!-- XmlFormatterBase Document Separator -->
