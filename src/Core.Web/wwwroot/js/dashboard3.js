/* global Chart:false */

$(function () {
  'use strict'

  var ticksStyle = {
    fontColor: '#495057',
    fontStyle: 'bold'
  }

  var mode = 'index'
  var intersect = true

  var $salesChart = $('#sales-chart')
  // eslint-disable-next-line no-unused-vars

 //Ajax

  var salesChart = new Chart($salesChart, {
    type: 'bar',
    data: {
      labels: ['SEGUNDA', 'TERCA', 'QUARTA', 'QUINTA', 'SEXTA', 'SABADO', 'DOMINGO'],
      datasets: [
        {
          backgroundColor: '#007bff',
          borderColor: '#007bff',
          data: [1000, 2000, 3000, 2500, 2700, 2500, 3000]
        },
        {
          backgroundColor: '#ced4da',
          borderColor: '#ced4da',
          data: [700, 1700, 2700, 2000, 1800, 1500, 2000]
        }
      ]
    },
    options: {
      maintainAspectRatio: false,
      tooltips: {
        mode: mode,
        intersect: intersect
      },
      hover: {
        mode: mode,
        intersect: intersect
      },
      legend: {
        display: false
      },
      scales: {
        yAxes: [{
          // display: false,
          gridLines: {
            display: true,
            lineWidth: '4px',
            color: 'rgba(0, 0, 0, .2)',
            zeroLineColor: 'transparent'
          },
          ticks: $.extend({
            beginAtZero: true,

            // Include a dollar sign in the ticks
            //callback: function (value) {
            //  if (value >= 1000) {
            //    value /= 1000
            //    value += 'k'
            //  }

            //  return '$' + value
            //}
          }, ticksStyle)
        }],
        xAxes: [{
          display: true,
          gridLines: {
            display: false
          },
          ticks: ticksStyle
        }]
      }
    }
  })
})

$(function () {
    /* ChartJS
     * -------
     * Here we will create a few charts using ChartJS
     */
    
    //-------------
    //- CARE PLAIN CHART - PIE CHART -
    //-------------
    // Get context with jQuery - using jQuery's .get() method.
    //var carePlainChartCanvas = $('#carePlainChart').get(0).getContext('2d')
    var pieChartFaixaEtariaCanvas = $('#pieChartFaixaEtaria').get(0).getContext('2d') 
    //var donutData = {
    //    labels: [
    //        'Viana do Castelo',
    //        'Braga',
    //        'Vila Real',
    //        'Braganca',
    //        'Porto',
    //        'Aveiro',
    //        'Viseu',
    //        'Guarda',
    //        'Coimbra',
    //        'Castelo Branco',
    //        'Lisboa',
    //        'Santarem',
    //        'Portalegre',
    //        'Evora',
    //        'Beja',
    //        'Faro',
    //    ],
    //    datasets: [
    //        {
    //            data: [20, 35, 05, 13, 101, 86, 20, 35, 05, 13, 101, 34, 5, 9, 01, 22],
    //            backgroundColor: ['#f56954', '#00a65a', '#f39c12', '#00c0ef', '#3c8dbc', '#d2d6de', '#81b94a', '#81644a', '#51644a', '#516400', '#510000', '#f18973', '#d5f4e6', '#80ced6', '#fefbd8', '#05ffb0'],
    //        }
    //    ]
    //}

    var pieChartDataFaixaEtaria = {
        labels: [
            'Ate 18',
            '18 a 45',
            '45 a 65',
            'Acima de 65',
        ],
        datasets: [
            {
                data: [700, 500, 400, 600, 300, 100],
                backgroundColor: ['#f56954', '#00a65a', '#f39c12', '#00c0ef', '#3c8dbc', '#d2d6de'],
            }
        ]
    }

    //var pieData = donutData;
    var pieDataFaixaEtaria = pieChartDataFaixaEtaria;
    //var donutDataFaixaEtaria = donutChartDataFaixaEtaria;
    var pieOptions = {
        maintainAspectRatio: false,
        responsive: true,
    }
    //Create pie or douhnut chart
    // You can switch between pie and douhnut using the method below.
    //var pieChart = new Chart(pieChartCanvas, {
    //    type: 'pie',
    //    data: pieData,
    //    options: pieOptions
    //})

    var pieChartFaixaEtaria = new Chart(pieChartFaixaEtariaCanvas, {
        type: 'pie',
        data: pieDataFaixaEtaria,
        options: pieOptions
    })
})

$(function () {
    var ctx = $("#myChart").get(0).getContext('2d');
    ctx = $("#myChart");
    // examine example_data.json for expected response data
    var json_url = "/Dashboard/InquiryChartGet";

    // draw empty chart
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: [],
            datasets: [

            ]
        },
        options: {
            tooltips: {
                mode: 'index',
                intersect: false
            },
            legend: {
                display: false
            },
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });

    ajax_chart(myChart, json_url);

    // function to update our chart
    function ajax_chart(chart, url, data) {
        var data = data || {};

        $.getJSON(url, data).done(function (response) {
            console.log(response);
            chart.data = response.data;
            //chart.data.datasets[1].data = x.data[1].quantity; // or you can iterate for multiple datasets
            chart.update(); // finally update our chart
        });
    }
});

$(function () {
    var carePlain = $("#carePlainChart").get(0).getContext('2d');
    carePlain = $("#carePlainChart");
    // examine example_data.json for expected response data
    var json_url = "/Dashboard/CarePlainChartGet";

    var carePlainChart = new Chart(carePlain, {
        type: 'pie',
        data: {
            labels: [],
            datasets: [

            ]
        },
        options: {
            maintainAspectRatio: false,
            responsive: true,
        }
    })

    ajax_chart(carePlainChart, json_url);

    // function to update our chart
    function ajax_chart(chart, url, data) {
        var data = data || {};

        $.getJSON(url, data).done(function (response) {
            console.log(response);
            chart.data = response.data;
            //chart.data.datasets[1].data = x.data[1].quantity; // or you can iterate for multiple datasets
            chart.update(); // finally update our chart
        });
    }
});

$(function () {
    var patientsAgeGroup = $("#patientsAgeGroupChart").get(0).getContext('2d');
    patientsAgeGroup = $("#patientsAgeGroupChart");
    // examine example_data.json for expected response data
    var json_url = "/Dashboard/PatientsAgeGroupChartGet";

    var patientsAgeGroupChart = new Chart(patientsAgeGroup, {
        type: 'pie',
        data: {
            labels: [],
            datasets: [

            ]
        },
        options: {
            maintainAspectRatio: false,
            responsive: true,
        }
    })

    ajax_chart(patientsAgeGroupChart, json_url);

    // function to update our chart
    function ajax_chart(chart, url, data) {
        var data = data || {};

        $.getJSON(url, data).done(function (response) {
            console.log(response);
            chart.data = response.data;
            //chart.data.datasets[1].data = x.data[1].quantity; // or you can iterate for multiple datasets
            chart.update(); // finally update our chart
        });
    }
});
