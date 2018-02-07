// Create scene
var scene = new THREE.Scene();

// Create camera
var camera = new THREE.PerspectiveCamera(
	75, // fov — Camera frustum vertical field of view.
	window.innerWidth/window.innerHeight, // aspect — Camera frustum aspect ratio.
	0.1, // near — Camera frustum near plane.
	10000); // far — Camera frustum far plane.

// Create renderer
var renderer = new THREE.WebGLRenderer();
renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);

var geometry = new THREE.BoxBufferGeometry( 1, 1, 1 );
//var material = new THREE.MeshNormalMaterial();

var texture = new THREE.TextureLoader().load( "./ModelsAndTextures/Yellobrk.bmp" );
var geometry = new THREE.BoxBufferGeometry( 200, 200, 200 );
var material = new THREE.MeshBasicMaterial( { map: texture } );

var cube = new THREE.Mesh(geometry, material);
scene.add(cube);

// TODO remove; light used for debugging
var ambientLight = new THREE.AmbientLight(0x404040);
scene.add(ambientLight);

// Move camera from center
camera.position.x = 600;
camera.position.y = 200; // height
camera.position.z = 1000;

var skybox = createSkybox(5000);
scene.add(skybox);

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