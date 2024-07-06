export const setChartOptions = () => {
  return {
    plugins: {
      legend: {
        labels: {
          color: '#ffffff80',
        },
      },
    },
    scales: {
      x: {
        ticks: {
          color: '#ffffff70',
        },
        grid: {
          color: '#ffffff10',
        },
      },
      y: {
        beginAtZero: true,
        ticks: {
          color: '#ffffff70',
        },
        grid: {
          color: '#ffffff10',
        },
      },
    },
  };
};
