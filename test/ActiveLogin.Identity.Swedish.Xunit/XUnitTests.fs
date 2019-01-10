module Tests

open System
open Xunit
open ActiveLogin.Identity.Swedish

[<Literal>]
let InvalidSwedishPersonalIdentityNumberErrorMessage = "Invalid Swedish personal identity number."

[<Theory>]
[<InlineData("900101+9802", 1890)>]
[<InlineData("990913+9801", 1899)>]
[<InlineData("120211+9986", 1912)>]
let Parses_Year_From_10_Digit_String_When_Plus_Is_Delimiter(personalIdentityNumberString, expectedYear) =
    let personalIdentityNumber = SwedishPersonalIdentityNumber.ParseInSpecificYear(personalIdentityNumberString, 2012)
    Assert.Equal(expectedYear, personalIdentityNumber.Year)

[<Theory>]
[<InlineData("900101+9802", 1890)>]
let Parses_Year_From_10_Digit_String_When_Year_Is_Exact_100_Years(personalIdentityNumberString, expectedYear) =
    let personalIdentityNumber = SwedishPersonalIdentityNumber.ParseInSpecificYear(personalIdentityNumberString, 1990)
    Assert.Equal(expectedYear, personalIdentityNumber.Year)

[<Theory>]
[<InlineData("990807-2391", 1999)>]
[<InlineData("180101-2392", 2018)>]
let Parses_Year_From_10_Digit_String_When_Dash_Is_Delimiter(personalIdentityNumberString, expectedYear) =
    let personalIdentityNumber = SwedishPersonalIdentityNumber.ParseInSpecificYear(personalIdentityNumberString, 2018)
    Assert.Equal(expectedYear, personalIdentityNumber.Year)

[<Theory>]
[<InlineData("990913+9801", 1899)>]
[<InlineData("120211+9986", 1912)>]
[<InlineData("990807-2391", 1999)>]
[<InlineData("180101-2392", 2018)>]
let Parses_Year_From_10_Digit_String(personalIdentityNumberString, expectedYear) =
    let personalIdentityNumber = SwedishPersonalIdentityNumber.Parse(personalIdentityNumberString)
    Assert.Equal(expectedYear, personalIdentityNumber.Year)

[<Theory>]
[<InlineData("990913+9801", 09)>]
[<InlineData("120211+9986", 02)>]
[<InlineData("990807-2391", 08)>]
[<InlineData("180101-2392", 01)>]
let Parses_Month_From_10_Digit_String(personalIdentityNumberString, expectedMonth) =
    let personalIdentityNumber = SwedishPersonalIdentityNumber.Parse(personalIdentityNumberString)
    Assert.Equal(expectedMonth, personalIdentityNumber.Month)

[<Theory>]
[<InlineData("990913+9801", 13)>]
[<InlineData("120211+9986", 11)>]
[<InlineData("990807-2391", 07)>]
[<InlineData("180101-2392", 01)>]
let Parses_Day_From_10_Digit_String(personalIdentityNumberString, expectedDay) =
    let personalIdentityNumber = SwedishPersonalIdentityNumber.Parse(personalIdentityNumberString)
    Assert.Equal(expectedDay, personalIdentityNumber.Day)

[<Theory>]
[<InlineData("990913+9801", 980)>]
[<InlineData("120211+9986", 998)>]
[<InlineData("990807-2391", 239)>]
[<InlineData("180101-2392", 239)>]
let Parses_BirthNumber_From_10_Digit_String(personalIdentityNumberString, expectedBirthNumber) =
    let personalIdentityNumber = SwedishPersonalIdentityNumber.Parse(personalIdentityNumberString)
    Assert.Equal(expectedBirthNumber, personalIdentityNumber.BirthNumber)

[<Theory>]
[<InlineData("990913+9801", 1)>]
[<InlineData("120211+9986", 6)>]
[<InlineData("990807-2391", 1)>]
[<InlineData("180101-2392", 2)>]
let Parses_Checksum_From_10_Digit_String(personalIdentityNumberString, expectedChecksum) =
    let personalIdentityNumber = SwedishPersonalIdentityNumber.Parse(personalIdentityNumberString)
    Assert.Equal(expectedChecksum, personalIdentityNumber.Checksum)

[<Theory>]
[<InlineData(" 990913+9801 ", "990913+9801")>]
[<InlineData(" 990807-2391", "990807-2391")>]
[<InlineData("180101-2392 ", "180101-2392")>]
let Strips_Leading_And_Trailing_Whitespace_From_10_Digit_String(personalIdentityNumberString,
                                                                expectedPersonalIdentityNumberString) =
    let personalIdentityNumber = SwedishPersonalIdentityNumber.ParseInSpecificYear(personalIdentityNumberString, 2018)
    Assert.Equal(expectedPersonalIdentityNumberString, personalIdentityNumber.To10DigitStringInSpecificYear(2018))

[<Theory>]
[<InlineData("990913�9801")>]
[<InlineData("990913_9801")>]
[<InlineData("990913.9801")>]
let Throws_ArgumentException_When_Invalid_Delimiter_From_10_Digit_String(personalIdentityNumberString) =
    let action =
        (fun () -> SwedishPersonalIdentityNumber.ParseInSpecificYear(personalIdentityNumberString, 2018) |> ignore)
    let ex = Assert.Throws<ArgumentException>(action)
    Assert.Contains(InvalidSwedishPersonalIdentityNumberErrorMessage, ex.Message)

[<Theory>]
[<InlineData("189909139801", 1899)>]
[<InlineData("191202119986", 1912)>]
let Parses_Year_From_12_Digit_String_When_Plus_Is_Delimiter(personalIdentityNumberString, expectedYear) =
    let personalIdentityNumber = SwedishPersonalIdentityNumber.ParseInSpecificYear(personalIdentityNumberString, 2018)
    Assert.Equal(expectedYear, personalIdentityNumber.Year)

[<Theory>]
[<InlineData("199908072391", 1999)>]
[<InlineData("201801012392", 2018)>]
let Parses_Year_From_12_Digit_String_When_Dash_Is_Delimiter(personalIdentityNumberString, expectedYear) =
    let personalIdentityNumber = SwedishPersonalIdentityNumber.ParseInSpecificYear(personalIdentityNumberString, 2018)
    Assert.Equal(expectedYear, personalIdentityNumber.Year)

[<Theory>]
[<InlineData("189909139801", 1899)>]
[<InlineData("191202119986", 1912)>]
[<InlineData("199908072391", 1999)>]
[<InlineData("201801012392", 2018)>]
let Parses_Year_From_12_Digit_String(personalIdentityNumberString, expectedYear) =
    let personalIdentityNumber = SwedishPersonalIdentityNumber.Parse(personalIdentityNumberString)
    Assert.Equal(expectedYear, personalIdentityNumber.Year)

[<Theory>]
[<InlineData("189909139801", 09)>]
[<InlineData("191202119986", 02)>]
[<InlineData("199908072391", 08)>]
[<InlineData("201801012392", 01)>]
let Parses_Month_From_12_Digit_String(personalIdentityNumberString, expectedMonth) =
    let personalIdentityNumber = SwedishPersonalIdentityNumber.Parse(personalIdentityNumberString)
    Assert.Equal(expectedMonth, personalIdentityNumber.Month)

[<Theory>]
[<InlineData("189909139801", 13)>]
[<InlineData("191202119986", 11)>]
[<InlineData("199908072391", 07)>]
[<InlineData("201801012392", 01)>]
let Parses_Day_From_12_Digit_String(personalIdentityNumberString, expectedDay) =
    let personalIdentityNumber = SwedishPersonalIdentityNumber.Parse(personalIdentityNumberString)
    Assert.Equal(expectedDay, personalIdentityNumber.Day)

[<Theory>]
[<InlineData("189909139801", 980)>]
[<InlineData("191202119986", 998)>]
[<InlineData("199908072391", 239)>]
[<InlineData("201801012392", 239)>]
let Parses_BirthNumber_From_12_Digit_String(personalIdentityNumberString, expectedBirthNumber) =
    let personalIdentityNumber = SwedishPersonalIdentityNumber.Parse(personalIdentityNumberString)
    Assert.Equal(expectedBirthNumber, personalIdentityNumber.BirthNumber)

[<Theory>]
[<InlineData("189909139801", 1)>]
[<InlineData("191202119986", 6)>]
[<InlineData("199908072391", 1)>]
[<InlineData("201801012392", 2)>]
let Parses_Checksum_From_12_Digit_String(personalIdentityNumberString, expectedChecksum) =
    let personalIdentityNumber = SwedishPersonalIdentityNumber.Parse(personalIdentityNumberString)
    Assert.Equal(expectedChecksum, personalIdentityNumber.Checksum)

[<Theory>]
[<InlineData(" 189909139801 ", "189909139801")>]
[<InlineData(" 191202119986", "191202119986")>]
[<InlineData("199908072391 ", "199908072391")>]
let Strips_Leading_And_Trailing_Whitespace_From_12_Digit_String(personalIdentityNumberString,
                                                                expectedPersonalIdentityNumberString) =
    let personalIdentityNumber = SwedishPersonalIdentityNumber.ParseInSpecificYear(personalIdentityNumberString, 2018)
    Assert.Equal(expectedPersonalIdentityNumberString, personalIdentityNumber.To12DigitString())

[<Theory>]
[<InlineData("18990913-9801", "189909139801")>]
[<InlineData("19120211-9986", "191202119986")>]
[<InlineData("19990807-2391", "199908072391")>]
let Parses_When_Hyphen_Delimiter_From_12_Digit_String(personalIdentityNumberString, expectedPersonalIdentityNumberString) =
    let personalIdentityNumber = SwedishPersonalIdentityNumber.ParseInSpecificYear(personalIdentityNumberString, 2018)
    Assert.Equal(expectedPersonalIdentityNumberString, personalIdentityNumber.To12DigitString())

[<Theory>]
[<InlineData("18990913 9801", "189909139801")>]
[<InlineData("19120211 9986", "191202119986")>]
[<InlineData("19990807 2391", "199908072391")>]
let Parses_When_Whitespace_Delimiter_From_12_Digit_String(personalIdentityNumberString,
                                                          expectedPersonalIdentityNumberString) =
    let personalIdentityNumber = SwedishPersonalIdentityNumber.ParseInSpecificYear(personalIdentityNumberString, 2018)
    Assert.Equal(expectedPersonalIdentityNumberString, personalIdentityNumber.To12DigitString())

[<Theory>]
[<InlineData("180101 2392", "201801012392")>]
[<InlineData("990807 2391", "199908072391")>]
let Parses_When_Whitespace_Delimiter_From_10_Digit_String(personalIdentityNumberString,
                                                          expectedPersonalIdentityNumberString) =
    let personalIdentityNumber = SwedishPersonalIdentityNumber.ParseInSpecificYear(personalIdentityNumberString, 2018)
    Assert.Equal(expectedPersonalIdentityNumberString, personalIdentityNumber.To12DigitString())

[<Theory>]
[<InlineData("18990913+9801")>]
let Throws_ArgumentException_When_Plus_Delimiter_From_12_Digit_String(personalIdentityNumberString) =
    let action =
        (fun () -> SwedishPersonalIdentityNumber.ParseInSpecificYear(personalIdentityNumberString, 2018) |> ignore)
    let ex = Assert.Throws<ArgumentException>(action)
    Assert.Contains(InvalidSwedishPersonalIdentityNumberErrorMessage, ex.Message)

[<Theory>]
[<InlineData("990913�9801")>]
[<InlineData("19990913�9801")>]
[<InlineData("990913_9801")>]
[<InlineData("19990913_9801")>]
[<InlineData("990913.9801")>]
[<InlineData("19990913.9801")>]
let Throws_ArgumentException_When_Invalid_Delimiter(personalIdentityNumberString) =
    let ex =
        Assert.Throws<ArgumentException>
            (fun () -> SwedishPersonalIdentityNumber.ParseInSpecificYear(personalIdentityNumberString, 2018) |> ignore)
    Assert.Contains(InvalidSwedishPersonalIdentityNumberErrorMessage, ex.Message)

[<Fact>]
let Same_Number_Will_Use_Different_Delimiter_When_Parsed_On_Or_After_Person_Turns_100() =
    let withHyphen = "120211-9986"
    let withPlus = "120211+9986"
    let pinBeforeTurning100 = SwedishPersonalIdentityNumber.ParseInSpecificYear(withHyphen, 2011)
    let pinOnYearTurning100 = SwedishPersonalIdentityNumber.ParseInSpecificYear(withPlus, 2012)
    let pinAfterTurning100 = SwedishPersonalIdentityNumber.ParseInSpecificYear(withPlus, 2013)
    let expected = SwedishPersonalIdentityNumber.Create(1912, 02, 11, 998, 6)
    Assert.Equal(expected, pinBeforeTurning100)
    Assert.Equal(expected, pinOnYearTurning100)
    Assert.Equal(expected, pinAfterTurning100)

[<Fact>]
let Parses_When_Begins_With_Zero() =
    let personalIdentityNumber = SwedishPersonalIdentityNumber.Parse("000101-2384")
    Assert.Equal(SwedishPersonalIdentityNumber.Create(2000, 1, 1, 238, 4), personalIdentityNumber)

[<Fact>]
let Parses_When_Ends_With_Zero() =
    let personalIdentityNumber = SwedishPersonalIdentityNumber.Parse("170122-2380")
    Assert.Equal(SwedishPersonalIdentityNumber.Create(2017, 1, 22, 238, 0), personalIdentityNumber)

[<Fact>]
let Parse_Throws_ArgumentException_When_Empty_String() =
    let ex = Assert.Throws<ArgumentException>(fun () -> SwedishPersonalIdentityNumber.Parse("") |> ignore)
    Assert.Contains("", ex.Message)

[<Fact>]
let Parse_Throws_ArgumentException_When_Whitespace_String() =
    let ex = Assert.Throws<ArgumentException>(fun () -> SwedishPersonalIdentityNumber.Parse(" ") |> ignore)
    Assert.Contains("", ex.Message)

[<Fact>]
let Parse_Throws_ArgumentNullException_When_Null() =
    let ex = Assert.Throws<ArgumentNullException>(fun () -> SwedishPersonalIdentityNumber.Parse(null) |> ignore)
    Assert.Contains("", ex.Message)

[<Fact>]
let ParseInSpecificYear_Throws_ArgumentException_When_Empty_String() =
    let ex =
        Assert.Throws<ArgumentException>
            (fun () -> SwedishPersonalIdentityNumber.ParseInSpecificYear("", 2018) |> ignore)
    Assert.Contains("", ex.Message)

[<Fact>]
let ParseInSpecificYear_Throws_ArgumentException_When_Whitespace_String() =
    let ex =
        Assert.Throws<ArgumentException>
            (fun () -> SwedishPersonalIdentityNumber.ParseInSpecificYear(" ", 2018) |> ignore)
    Assert.Contains("", ex.Message)

[<Fact>]
let ParseInSpecificYear_Throws_ArgumentNullException_When_Null() =
    let ex =
        Assert.Throws<ArgumentNullException>
            (fun () -> SwedishPersonalIdentityNumber.ParseInSpecificYear(null, 2018) |> ignore)
    Assert.Contains("", ex.Message)
