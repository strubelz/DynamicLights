<root dataType="Struct" type="Duality.Resources.FragmentShader" id="129723834">
  <assetInfo dataType="Struct" type="Duality.Editor.AssetManagement.AssetInfo" id="427169525">
    <customData />
    <importerId dataType="String">BasicShaderAssetImporter</importerId>
    <sourceFileHint dataType="Array" type="System.String[]" id="1100841590">
      <item dataType="String">{Name}.frag</item>
    </sourceFileHint>
  </assetInfo>
  <source dataType="String">#define PI 3.14

uniform sampler2D mainTex;
uniform float lightSize;

//alpha threshold for our occlusion map
const float THRESHOLD = 0.75;

void main(void) {
  float distance = 1.0;
  
  for (float y=0.0; y&lt;lightSize; y+=1.0) {
  		//rectangular to polar filter
		vec2 norm = vec2(gl_TexCoord[0].s, y/lightSize) * 2.0 - 1.0;
		float theta = PI*1.5 + norm.x * PI; 
		float r = (1.0 + norm.y) * 0.5;
		
		//coord which we will sample from occlude map
		vec2 coord = vec2(-r * sin(theta), -r * cos(theta))/2.0 + 0.5;
		
		//sample the occlusion map
		vec4 data = texture2D(mainTex, coord);
		
		//the current distance is how far from the top we've come
		float dst = y/lightSize;
		
		//if we've hit an opaque fragment (occluder), then get new distance
		//if the new distance is below the current, then we'll use that for our ray
		float caster = data.a;
		if (caster &gt; THRESHOLD) {
			distance = min(distance, dst);
			//NOTE: we could probably use "break" or "return" here
  		}
  } 
  gl_FragColor = vec4(vec3(distance), 1.0);
}</source>
</root>
<!-- XmlFormatterBase Document Separator -->
