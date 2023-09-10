namespace GoogleFormsClient.Contracts;

/// <summary>
/// Available question types.
/// </summary>
public enum QuestionType
{
    /// <summary>
    /// Short answer
    /// </summary>
    ShortAnswerField = 0,

    /// <summary>
    /// Paragraph Field
    /// </summary>
    ParagraphField = 1,

    /// <summary>
    /// Multiple Choice Field"
    /// </summary>
    MultipleChoiceField = 2,

    /// <summary>
    /// Drop Down Field"
    /// </summary>
    DropDownField = 3,

    /// <summary>
    /// Check Boxes Field"
    /// </summary>
    CheckBoxesField = 4,
    
    /// <summary>
    /// Linear scale field
    /// </summary>
    LinearScaleField = 5,
    
    /// <summary>
    /// FileUpload - Not supported (needs user log in session)
    /// </summary>
    FileUploadField = 13,
    
    /// <summary>
    ///  represents both: Multiple Choice Grid | Checkbox Grid
    /// </summary>
    GridChoiceField = 7,

    /// <summary>
    /// Date field.
    /// </summary>
    DateField = 9,
    
    /// <summary>
    /// Time field
    /// </summary>
    TimeField = 10,
}