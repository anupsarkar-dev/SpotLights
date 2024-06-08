using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotLights.Shared.Helper;

/// <summary>
/// Set the response with success and error data
/// </summary>
/// <typeparam name="TValue">Success result</typeparam>
/// <typeparam name="TError">Error result</typeparam>
public readonly struct ApplicationResult<TValue, TError>
{
  private readonly TValue? _value;
  private readonly TError? _error;
  private readonly string? _message;

  /// <summary>
  /// Response for success
  /// </summary>
  /// <param name="value"></param>
  public ApplicationResult(TValue value)
  {
    IsError = false;
    _value = value;
    _error = default;
    _message = default;
  }

  /// <summary>
  /// Response for success with message
  /// </summary>
  /// <param name="value"></param>
  /// <param name="message"></param>
  public ApplicationResult(TValue value, string message)
  {
    IsError = false;
    _value = value;
    _error = default;
    _message = message;
  }

  /// <summary>
  /// Response for error
  /// </summary>
  /// <param name="error"></param>
  public ApplicationResult(TError error)
  {
    IsError = true;
    _error = error;
    _value = default;
    _message = default;
  }

  public bool IsError { get; }
  public bool IsSuccess => !IsSuccess;

  public static implicit operator ApplicationResult<TValue, TError>(TValue value) => new(value);
  public static implicit operator ApplicationResult<TValue, TError>(TError error) => new(error);

  /// <summary>
  /// Matching function to get result for the response
  /// </summary>
  /// <typeparam name="TResult"></typeparam>
  /// <param name="success"></param>
  /// <param name="error"></param>
  /// <returns>Returns success or failuer based on the error status</returns>
  public TResult Match<TResult>(Func<ApplicationSuccessResponse<TValue>, TResult> onSuccess, Func<TError, TResult> onFailed) =>
                      !IsError ? onSuccess(new ApplicationSuccessResponse<TValue>() { Data = _value, Message = _message }) : onFailed(_error);

  /// <summary>
  /// Get success result
  /// </summary>
  /// <returns>Return success result</returns>
  public TValue GetSuccessResult() => _value;

  /// <summary>
  /// Get error result
  /// </summary>
  /// <returns>Return error result</returns>
  public TError GetErrors() => _error;
}


/// <summary>
/// Application success response dto
/// </summary>
/// <typeparam name="TValue">Success result</typeparam>
public class ApplicationSuccessResponse<TValue>
{
  /// <summary>
  /// Success value to return
  /// </summary>
  public TValue? Data { get; set; }
  /// <summary>
  /// Success message to return
  /// </summary>
  public string? Message { get; set; }
}
