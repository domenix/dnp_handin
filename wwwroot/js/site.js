// Write your Javascript code.
$(function() {
  // page is now ready, initialize the calendar...

  $("#calendar").fullCalendar({
    defaultView: "agendaSevenDay",
    groupByResource: true,
    header: {
      right: "prev,next"
    },
    views: {
      agendaSevenDay: {
        type: "agenda",
        duration: { days: 7 }
      }
    },
    groupByResource: true,
    slotLabelFormat: "HH:mm",
    events: test,
    minTime: "10:00:00",
    maxTime: "24:00:00",
    defaultDate: $("#calendar").fullCalendar("today"),
    allDaySlot: false,
    contentHeight: "auto",
    nowIndicator: true,
    eventColor: "#9B5E5E"
  });
  $("document").ready(function() {
    console.log(test);
  });
});

function getScreenings() {
  return fetch("/api/screenings").then(res => res.json());
}

function getMovies() {
  return fetch("/api/movies").then(res => res.json());
}

function deleteScreening(id) {
  return fetch("/api/screenings/" + id, {
    method: "DELETE"
  });
}

async function createScreening(date, movieId) {
  if (!date) {
    throw new Error("You must enter a date");
  }
  if (Date.parse(date) < Date.now()) {
    throw new Error("Invalid date");
  }
  return fetch("/api/screenings/", {
    method: "POST",
    body: JSON.stringify({
      date,
      movieId
    })
  });
}

function deleteMovie(id) {
  return fetch("/api/movies/" + id, {
    method: "DELETE"
  });
}
async function createMovie(title, duration) {
  if (!title) {
    throw new Error("You must enter a title");
  }
  if (title.length >= 300) {
    throw new Error("Title length must be less than 300 characters");
  }
  if (!duration) {
    throw new Error("Invalid duration");
  }
  return fetch("/api/movies/", {
    method: "POST",
    body: JSON.stringify({
      title,
      duration
    })
  });
}

function buttonClicked()
{
  window.open("/CreateReservation");
}