angular.module('app.commonControllers', [])

function commonPartialService() {
    return {
        SubjectJournalJournalVersionCtrl: function () {
            return {
                BindSubject: function ($scope, onSubjectChangePostBack) {
                    $scope.bindSubject = function () {
                        $scope.subjects = subject.Items;
                        if (subject.UseSelect) {
                            $scope.subjects.unshift({ Text: selectItem, Value: null });
                        }
                        $scope.model.SubjectID = subject.SelectedItem;
                    }
                    $scope.onSubjectChange = function () {
                        //reset Journal value
                        if (journal != null && journal.Show) {
                            $scope.bindJournal();
                        }
                        if (onSubjectChangePostBack != null) {
                            onSubjectChangePostBack();
                        }
                    }
                },

                BindJournal: function ($scope, $filter, onJournalChangePostBack) {
                    $scope.bindJournal = function () {
                        if ($scope.model.SubjectID != null) {
                            $scope.journals = $filter('filter')(journal.Items, { Group: { Name: String($scope.model.SubjectID) } }, true);
                        } else {
                            $scope.journals = journal.Items;
                        }
                        if (journal.UseSelect) {
                            $scope.journals.unshift({ Text: selectItem, Value: null });
                        }
                        $scope.model.JournalID = journal.SelectedItem;
                    }

                    $scope.onJournalChange = function () {
                        //reset journal version value
                        if (journalVersion != null && journalVersion.Show) {
                            $scope.bindJournalVersion();
                        }
                        if (onJournalChangePostBack != null) {
                            onJournalChangePostBack();
                        }
                    }
                },

                BindJournalVersion: function ($scope, $filter, onJournalVersionChangePostBack) {
                    $scope.bindJournalVersion = function () {
                        if ($scope.model.JournalID != null) {
                            $scope.journalVersions = $filter('filter')(journalVersion.Items, { Group: { Name: String($scope.model.JournalID) } }, true);
                           
                        } else {
                            $scope.journalVersions = journalVersion.Items;
                        }
                        if (journalVersion.UseSelect) {
                            $scope.journalVersions.unshift({ Text: selectItem, Value: null });
                        }
                        $scope.model.JournalVersionID = journalVersion.SelectedItem;
                    }
                    $scope.onJournalVersionChange = function () {
                        if (onJournalVersionChangePostBack != null) {
                            onJournalVersionChangePostBack();
                        }
                    }
                }
            }
        }
    }

}



