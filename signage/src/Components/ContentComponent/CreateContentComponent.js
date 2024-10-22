import React from "react";

function CreateContentComponent() {
    return (
        <div className="container-fluid content-top-gap">
            <section class="forms">
                <div class="card card_border p-5 mb-4">
                    <h1 className="ml-3">Content Page</h1>
                    <div class="card-body">
                        <form action="#" method="post">
                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label for="id" class="input__label">
                                        Content Id
                                    </label>
                                    <input
                                        type="text"
                                        class="form-control input-style"
                                        id="id"
                                        placeholder="Content-id"
                                        required
                                    />
                                </div>
                                <div class="form-group col-md-6">
                                    <label for="title" class="input__label">
                                        Title
                                    </label>
                                    <input
                                        type="text"
                                        class="form-control input-style"
                                        id="title"
                                        placeholder="title"
                                        required
                                    />
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label for="filetype" class="input__label">
                                        File type
                                    </label>
                                    <input
                                        type="text"
                                        class="form-control input-style"
                                        id="filetype"
                                        placeholder="File Type"
                                        required
                                    />
                                </div>
                                <div class="form-group col-md-6">
                                    <label for="Select File" class="input__label">
                                        File Path
                                    </label>
                                    <input
                                        type="file"
                                        class="form-control input-style"
                                        id="path"
                                        placeholder="path"
                                        required
                                    />
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="url" class="input__label">
                                    URL
                                </label>
                                <input
                                    type="text"
                                    class="form-control input-style"
                                    id="url"
                                    placeholder="Url"
                                    required
                                />
                            </div>
                            <div class="form-check check-remember check-me-out">
                                <input
                                    class="form-check-input checkbox"
                                    type="checkbox"
                                    id="gridCheck"
                                />
                                <label class="form-check-label checkmark" for="gridCheck">
                                    Check me out
                                </label>
                            </div>
                            <button type="submit" class="btn btn-primary btn-style mt-4">
                                Add Content
                            </button>
                        </form>
                    </div>
                </div>
            </section>
        </div>
    );
}

export default CreateContentComponent;