$(function () {
    // =====================================
    // Profit
    // =====================================
    var profit = {
        series: [
            {
                name: "Số sản phẩm được bán ra",
                data: dashboardData.totalSelledData,
            },
        ],
        chart: {
            fontFamily: "Poppins,sans-serif",
            type: "bar",
            height: 370,
            offsetY: 10,
            toolbar: {
                show: false,
            },
        },
        grid: {
            show: true,
            strokeDashArray: 3,
            borderColor: "rgba(0,0,0,.1)",
        },
        colors: ["#1e88e5", "#21c1d6"],
        plotOptions: {
            bar: {
                horizontal: false,
                columnWidth: "30%",
                endingShape: "flat",
            },
        },
        dataLabels: {
            enabled: false,
        },
        stroke: {
            show: true,
            width: 5,
            colors: ["transparent"],
        },
        xaxis: {
            type: "category",
            categories: ["Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7", "Chủ Nhật"],
            axisTicks: {
                show: false,
            },
            axisBorder: {
                show: false,
            },
            labels: {
                style: {
                    colors: "#a1aab2",
                },
            },
        },
        yaxis: {
            labels: {
                style: {
                    colors: "#a1aab2",
                },
            },
        },
        fill: {
            opacity: 1,
            colors: ["#0085db", "#fb977d"],
        },
        tooltip: {
            theme: "dark",
        },
        legend: {
            show: false,
        },
        responsive: [
            {
                breakpoint: 767,
                options: {
                    stroke: {
                        show: false,
                        width: 5,
                        colors: ["transparent"],
                    },
                },
            },
        ],
    };

    console.log(profit);

    var chart_column_basic = new ApexCharts(
        document.querySelector("#profit"),
        profit
    );
    chart_column_basic.render();


    // =====================================
    // Breakup
    // =====================================
    var grade = {
        series: [dashboardData.totalRevenue, dashboardData.totalImportPrice],
        labels: ["Tiền bán hàng", "Tiền nhập hàng"],
        chart: {
            height: 170,
            type: "donut",
            fontFamily: "Plus Jakarta Sans', sans-serif",
            foreColor: "#c6d1e9",
        },

        tooltip: {
            theme: "dark",
            fillSeriesColor: false,
        },

        colors: ["#fb977d", "#0085db"],
        dataLabels: {
            enabled: false,
        },

        legend: {
            show: false,
        },

        stroke: {
            show: false,
        },
        responsive: [
            {
                breakpoint: 991,
                options: {
                    chart: {
                        width: 150,
                    },
                },
            },
        ],
        plotOptions: {
            pie: {
                donut: {
                    size: '80%',
                    background: "none",
                    labels: {
                        show: true,
                        name: {
                            show: true,
                            fontSize: "12px",
                            color: undefined,
                            offsetY: 5,
                        },
                        value: {
                            show: false,
                            color: "#98aab4",
                        },
                    },
                },
            },
        },
        responsive: [
            {
                breakpoint: 1476,
                options: {
                    chart: {
                        height: 120,
                    },
                },
            },
            {
                breakpoint: 1280,
                options: {
                    chart: {
                        height: 170,
                    },
                },
            },
            {
                breakpoint: 1166,
                options: {
                    chart: {
                        height: 120,
                    },
                },
            },
            {
                breakpoint: 1024,
                options: {
                    chart: {
                        height: 170,
                    },
                },
            },
        ],
    };

    var chart = new ApexCharts(document.querySelector("#grade"), grade);
    chart.render();



    // =====================================
    // Earning
    // =====================================
    var earning = {
        chart: {
            id: "sparkline3",
            type: "area",
            height: 60,
            sparkline: {
                enabled: true,
            },
            group: "sparklines",
            fontFamily: "Plus Jakarta Sans', sans-serif",
            foreColor: "#adb0bb",
        },
        series: [
            {
                name: "Bán được",
                color: "#8763da",
                data: dashboardData.totalSelledByMonth,
            },
        ],
        stroke: {
            curve: "smooth",
            width: 2,
        },
        fill: {
            colors: ["#f3feff"],
            type: "solid",
            opacity: 0.05,
        },

        markers: {
            size: 0,
        },
        tooltip: {
            theme: "dark",
            fixed: {
                enabled: true,
                position: "right",
            },
            x: {
                show: false,
            },
        },
    };

    console.log(earning);

    new ApexCharts(document.querySelector("#earning"), earning).render();
})