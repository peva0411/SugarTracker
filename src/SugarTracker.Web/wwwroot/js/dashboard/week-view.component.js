(function () {
  'use strict';


  function fetchWeek($http, date) {
    return $http.get("/api/weekview/" + date)
      .then(function(response) {
        return response.data;
      });
  }

  function getStartOfWeek(moment) {

    var startDate = moment().add(-1, 'day').startOf('week').add(1, 'day').utc();
    return startDate.format("YYYY-M-D");
  }
   

  function controller($http, moment) {
    var vm = this;

    vm.$onInit = function() {

      var date = getStartOfWeek(moment);
      vm.date = date;

      fetchWeek($http, date).then(function(weekview) {
        vm.weekView = weekview;
      });
    };

    vm.save = function(item, type) {
      console.log(item);
      var addedReading = item[type];
      addedReading.edit = false;

      var date = moment(item.date);
      var readingTime = moment(addedReading.readingTime, 'h:mm');
      date.add(readingTime.hours(), "hours");
      date.add(readingTime.minutes(), "minutes");
      date.utc();

      $http.post("/api/readings/", {
        Type: type,
        Value: addedReading.value,
        ReadingTime: date.toDate(),
        Notes: addedReading.notes
      });
    }
  };

  angular.module('dashboard').component('weekView', {

    templateUrl: '/js/dashboard/week-view.component.html',
    controllerAs: 'vm',
    controller: ["$http", "moment", controller] 
  });

})();