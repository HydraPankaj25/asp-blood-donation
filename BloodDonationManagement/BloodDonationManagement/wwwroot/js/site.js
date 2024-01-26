// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



const text = document.getElementById('text');
const prog = '"Be a saviour just by donating your blood, and be the reason for someones existence ...."';
let idx = 1;

setInterval(typingText, 150);

function typingText() {
	text.innerText = prog.slice(0, idx);
	idx++;

	if (idx > prog.length) {
		idx = 1;
	}
}


$('.icon').click(function () {
	$('.menu1').slideToggle(500);
});

//checking or matching password into reguistartion page


function checkpass() {
	let pass1 = document.getElementById("password").value;
	let pass2 = document.getElementById("password1").value;

	if (pass1 != pass2) {
		document.getElementById("warning").style.display = "block";
	}
	if (pass1 == pass2) {
		document.getElementById("warning").style.display = "none";
	}
}
