// Write your Javascript code.
$(function() {

    // page is now ready, initialize the calendar...
  
    $('#calendar').fullCalendar({
      eventClick: function(calEvent, jsEvent, view) {

        alert('Event: ' + calEvent.title);
        alert('Coordinates: ' + jsEvent.pageX + ',' + jsEvent.pageY);
        alert('View: ' + view.name);
    
        // change the border color just for fun
        $(this).css('border-color', 'red');
    
      },
      defaultView: 'basicWeek',
      height: 650,
      firstDay: 1
    })
    $('document').ready(function(){console.log(json2);});
    
  });