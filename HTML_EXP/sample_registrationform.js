		alert("Hello from JS");
	//const overlay = document.getElementById('overlay');	
    $(document).ready(function () {
			
		$('#profile').hide();
	   
        $('#home-tab').click(function (e) {
            e.preventDefault();
            $('#home').show();
			$('#profile').hide();
        });

        $('#profile-tab').click(function (e) {
            e.preventDefault();
            $('#profile').show();
			$('#home').hide();
        });
    });

//document.getElementById('overlay').style.display = 'block';
if($('#registration-toast'))
{
$('#registration-toast').show();
highlightLabel('registration-toast');
}
//if()


	function highlightLabel(elementId) {
	const element = document.getElementById(elementId);
	//element.classList.add('highlight');'
	if(element)
		{
			element.classList.add('highlight');
		}
	}
	
function hideoverlay()
{
	document.getElementById('overlay').style.display ='none';
	
}
document.addEventListener('click', function(){
    hideoverlay();
});
document.addEventListener('data-dismiss', function(){
	console.log("Inside the Addeventlistener: data-dismiss");
	hideoverlay();
});
	document.addEventListener('DOMContentLoaded', function() 
	{
		const overlay = document.getElementById('overlay');
		console.log("Overlay value on 20th line"+overlay);
		if(overlay)
		{
			document.getElementById('overlay').style.display='block';
		}
	});
