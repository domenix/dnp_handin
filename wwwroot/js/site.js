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

function getMoviesWithScreenings() {
  return fetch("/api/movies/withScreenings").then(res => res.json());
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
  }).then(res => {
    if (res.status === 400) throw new Error("Overlapping date");
    if (res.status !== 200) throw new Error("Unknown network error");
  });
}
async function createTicket(screeningId, userId, Seat) {
  if (!userId) {
    throw new Error("Invalid user");
  }
  if (!Seat) {
    throw new Error("Seat not selected");
  }
  if (!screeningId) {
    throw new Error("Invalid screening");
  }
  return fetch("/api/tickets/", {
    method: "POST",
    body: JSON.stringify({
      screeningId,
      userId,
      Seat
    })
  }).then(res => {
    if (res.status === 400) throw new Error("Invalid seat selection");
    if (res.status !== 200) throw new Error("Unknown network error");
    alert("Ticket reserved succesfully!");
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

function buttonClicked() {
  window.open("/CreateReservation","_self")
}
