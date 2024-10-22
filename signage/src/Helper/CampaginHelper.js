import Service from "../Services/service";
export async function CampaginListHooks(
  searchTerm,
  sortBy,
  sortDescending,
  pageNumber,
  pageSize
) {
  var data;
  await Service.getdata(
    "Campagin/GetAll?searchTerm=" +
      searchTerm +
      "&sortBy=" +
      sortBy +
      "&sortDescending=" +
      sortDescending +
      "&pageNumber=" +
      pageNumber +
      "&pageSize=" +
      pageSize
  ).then((res) => {
    data = res.data;
  });
  return data;
}
