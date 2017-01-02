// Write your Javascript code.

$(document).
            on('click', '.js-toggle-join',
                function (e) {
                    var button = $(e.target);
                    $.post("/api/assignments/join", { workid: button.attr("data-work-id") })
                        .done(function (
                        ) {
                            button.removeClass("js-toggle-join");
                            button.removeClass("btn-info");
                            button.addClass("btn-warning");
                            button.addClass("js-toggle-drop");
                            button.text("Leave");

                        })
                        .fail(function () {
                            alert("Something not right");
                        });
                })
            .on('click', '.js-toggle-drop',
                function (e) {
                    var button = $(e.target);
                    bootbox.confirm({
                        message: "Are you sure to give up this work",
                        buttons: {
                            confirm: {
                                label: 'Yes',
                                className: 'btn-danger'
                            },
                            cancel: {
                                label: 'No',
                                className: 'btn-default'
                            }
                        },
                        callback: function (result) {
                            if (result) {
                                $.post("/api/assignments/drop", { workid: button.attr("data-work-id") })
                                    .done(function (
                                    ) {
                                        button.removeClass("js-toggle-drop");
                                        button.removeClass("btn-warning");
                                        button.addClass("btn-info");
                                        button.addClass("js-toggle-join");
                                        button.text("Join");

                                    })
                                    .fail(function () {
                                        alert("Something not right");
                                    });
                            } else {
                                bootbox.hideAll();
                            }
                        }
                    });
                });
