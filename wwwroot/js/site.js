// Write your Javascript code.
$(function() {

    // page is now ready, initialize the calendar...
  
    $('#calendar').fullCalendar({
      defaultView: 'agendaSevenDay',
      groupByResource: true,
      header: {
        right: 'prev,next',
      },
      views: {
        agendaSevenDay: {
          type: 'agenda',
          duration: { days: 7 }
        }
      },
      groupByResource: true,
      slotLabelFormat:"HH:mm",
      events: test,
      minTime: "10:00:00",
      maxTime: "24:00:00",
      defaultDate: $('#calendar').fullCalendar('today'),
      allDaySlot: false,
      contentHeight: 'auto',
      nowIndicator: true,
      eventColor: '#9B5E5E'
    });
    $('document').ready(function(){console.log(test);});
    
  });