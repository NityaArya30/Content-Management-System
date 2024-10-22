import React from "react";

function CreateCampaginComponent() {
  return (
    <div className="container-fluid content-top-gap">
      <section class="forms">
        <div class="card card_border p-5 mb-4">
          <h1 className="ml-3">Campagin Form</h1>
          <div class="card-body">
            <form action="#" method="post">
              <div class="form-row">
                <div class="form-group col-md-6">
                  <label for="Priority" class="input__label">
                    Priority
                  </label>
                  <select
                    id="Priority"
                    name="priority"
                    class="form-control input-style"
                  >
                    <option>Select Priority</option>
                    <option value="1">Low</option>
                    <option value="2">Medium</option>
                    <option value="3">High</option>
                  </select>
                </div>
                <div class="form-group col-md-6">
                  <label for="reccurence" class="input__label">
                    Reccurence
                  </label>
                  <input
                    type="text"
                    class="form-control input-style"
                    id="reccurence"
                    placeholder="Reccurence"
                    required
                  />
                </div>
              </div>
              <div class="form-row">
                <div class="form-group col-md-6">
                  <label for="time" class="input__label">
                    Start Time
                  </label>
                  <input
                    type="time"
                    class="form-control input-style"
                    id="time"
                    placeholder="Start Time"
                    required
                  />
                </div>
                <div class="form-group col-md-6">
                  <label for="Etime" class="input__label">
                    End Time
                  </label>
                  <input
                    type="time"
                    class="form-control input-style"
                    id="Etime"
                    placeholder="End Time"
                    required
                  />
                </div>
              </div>

              <button type="submit" class="btn btn-primary btn-style mt-4">
                Add Campagin
              </button>
            </form>
          </div>
        </div>
      </section>
    </div>
  );
}

export default CreateCampaginComponent;
