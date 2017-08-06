Hint
====
Unit tests for the podcast modifications are not perfect. They depend on the implementation of the ContentActions and ContentFilters.

Another approach would be to give the abstract BaseModification a public constructor (thous requiring every implementation of the class to hide the constructor). This would allow for the testing of the BaseModification with mocks and eleminate the need to test the actual implementations.