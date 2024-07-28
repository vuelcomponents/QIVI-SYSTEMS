export default (timeDifference: number) => {
  const cdh = Math.floor(timeDifference / (1000 * 60 * 60));
  const cdm = Math.floor((timeDifference / (1000 * 60)) % 60);
  const cds = Math.floor((timeDifference / 1000) % 60);
  return {
    cds,
    cdm,
    cdh,
  };
};
