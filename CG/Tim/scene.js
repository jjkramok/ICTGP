// Create scene
var scene = new THREE.Scene();

// Create camera
var camera = new THREE.PerspectiveCamera(
	75, // fov — Camera frustum vertical field of view.
	window.innerWidth/window.innerHeight, // aspect — Camera frustum aspect ratio.
	0.1, // near — Camera frustum near plane.
	1000); // far — Camera frustum far plane. 

// Create renderer
var renderer = new THREE.WebGLRenderer({ antialias: true, alpha: true });
renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);

var geometry = new THREE.BoxGeometry(1, 1, 1);
//var material = new THREE.MeshNormalMaterial();
var material = new THREE.MeshPhongMaterial({color: 0x2d73a0, shininess: 100});
var cube = new THREE.Mesh(geometry, material);
scene.add(cube);

var light = new THREE.DirectionalLight(0xdddddd, 1);
light.position.set(0, 0, 1);
scene.add(light);

// Move camera from center
camera.position.x = 2;
camera.position.y = 1; // height
camera.position.z = 5;

// UV Mapping
var geometry1 = new THREE.Geometry();
geometry1.vertices.push( new THREE.Vector3( 0.0, 0.0, 0,0));
geometry1.vertices.push( new THREE.Vector3( 1.0, 0.0, 0,0));
geometry1.vertices.push( new THREE.Vector3( 1.0, 1.0, 0,0));
geometry1.vertices.push( new THREE.Vector3( 0.0, 1.0, 0,0));

var uvs = [];
uvs.push( new THREE.Vector2( 0.0, 0.0 ));
uvs.push( new THREE.Vector2( 1.0, 0.0 ));
uvs.push( new THREE.Vector2( 1.0, 1.0 ));
uvs.push( new THREE.Vector2( 0.0, 1.0 ));

// generate faces (triangles)
geometry1.faces.push( new THREE.Face3( 0, 1, 2 ));
geometry1.faces.push( new THREE.Face3( 2, 3, 0 ));
geometry1.faceVertexUvs[0].push([uvs[0], uvs[1], uvs[2]]);
geometry1.faceVertexUvs[0].push([uvs[2], uvs[3], uvs[0]]);
// End UV Mapping

// Normal Mapping
var baseImport = "./../ModelsAndTextures/";
var earthGeometry = new THREE.SphereGeometry(1, 32, 24);
normalMap = THREE.ImageUtils.loadTexture(baseImport + "earth_normal.jpg");
colorMap = THREE.ImageUtils.loadTexture(baseImport + "earth.jpg");

// Add animation
var clock = new THREE.Clock();

var render = function () {
    requestAnimationFrame(render);
    var delta = clock.getDelta();

    cube.rotation.x += 1.2 * delta;
    cube.rotation.y += 1.2 * delta;
    cube.rotation.z += .2 * delta;

    renderer.render(scene, camera);
};

render();