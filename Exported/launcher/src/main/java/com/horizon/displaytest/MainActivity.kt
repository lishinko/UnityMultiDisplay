package com.horizon.displaytest

import android.content.Intent
import android.content.res.Configuration
import android.os.Bundle
import android.util.Log
import android.view.KeyEvent
import android.view.MotionEvent
import android.view.SurfaceView
import android.widget.RadioGroup
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import com.UnityTechnologies.com.unity.template.urpblank.R
import com.UnityTechnologies.com.unity.template.urpblank.databinding.ActivityMainBinding
import com.unity3d.player.IUnityPlayerLifecycleEvents
import com.unity3d.player.MultiWindowSupport
import com.unity3d.player.UnityPlayer

class MainActivity : AppCompatActivity(), IUnityPlayerLifecycleEvents {
    val TAG = "MainActivity"
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()

        val item = ActivityMainBinding.inflate(layoutInflater)
        mUnityPlayer = UnityPlayer(this)
        item.flContent.addView(mUnityPlayer)
        mUnityPlayer.resume()
        mUnityPlayer.requestFocus()

        item.displayController.setOnCheckedChangeListener { _, checkedId ->
            run {
                val id = if(item.display0.id == checkedId) {
//                    item.flContent.id
                    1
                } else{
//                    item.view2.id
                    1
                }
                val surface = if (id == 1) {
                    item.view2.holder.surface
                } else {
                    null
                }
                Log.e(TAG, "onCreate: id = $checkedId, id = $id")
                mUnityPlayer.displayChanged(id, surface)
            }
        }
        setContentView(item.main)
    }

    override fun onNewIntent(intent: Intent) {
        super.onNewIntent(intent)
        mUnityPlayer.newIntent(intent)
    }

    override fun onDestroy() {
        super.onDestroy()
        mUnityPlayer.destroy()
    }

    override fun onStop() {
        super.onStop()

        if (!MultiWindowSupport.getAllowResizableWindow(this)) return

        mUnityPlayer.pause()
    }

    override fun onStart() {
        super.onStart()

        if (!MultiWindowSupport.getAllowResizableWindow(this)) return

        mUnityPlayer.resume()
    }

    // Pause Unity
    override fun onPause() {
        super.onPause()

        MultiWindowSupport.saveMultiWindowMode(this)

        if (MultiWindowSupport.getAllowResizableWindow(this)) return

        mUnityPlayer.pause()
    }

    // Resume Unity
    override fun onResume() {
        super.onResume()

        if (MultiWindowSupport.getAllowResizableWindow(this) && !MultiWindowSupport.isMultiWindowModeChangedToTrue(
                this
            )
        ) return

        mUnityPlayer.resume()
    }

    // Low Memory Unity
    override fun onLowMemory() {
        super.onLowMemory()
        mUnityPlayer.lowMemory()
    }

    // Trim Memory Unity
    override fun onTrimMemory(level: Int) {
        super.onTrimMemory(level)
        if (level == TRIM_MEMORY_RUNNING_CRITICAL) {
            mUnityPlayer.lowMemory()
        }
    }

    // This ensures the layout will be correct.
//    override fun onConfigurationChanged(newConfig: Configuration?) {
//        super.onConfigurationChanged(newConfig!!)
//        mUnityPlayer.configurationChanged(newConfig)
//    }

    // Notify Unity of the focus change.
    override fun onWindowFocusChanged(hasFocus: Boolean) {
        super.onWindowFocusChanged(hasFocus)
        mUnityPlayer.windowFocusChanged(hasFocus)
    }

    // For some reason the multiple keyevent type is not supported by the ndk.
    // Force event injection by overriding dispatchKeyEvent().
    override fun dispatchKeyEvent(event: KeyEvent): Boolean {
        if (event.action == KeyEvent.ACTION_MULTIPLE) return mUnityPlayer.injectEvent(event)
        return super.dispatchKeyEvent(event)
    }

    // Pass any events not handled by (unfocused) views straight to UnityPlayer
    override fun onKeyUp(keyCode: Int, event: KeyEvent?): Boolean {
        return mUnityPlayer.onKeyUp(keyCode, event)
    }

    override fun onKeyDown(keyCode: Int, event: KeyEvent?): Boolean {
        return mUnityPlayer.onKeyDown(keyCode, event)
    }

    override fun onTouchEvent(event: MotionEvent?): Boolean {
        return mUnityPlayer.onTouchEvent(event)
    }

    override fun onGenericMotionEvent(event: MotionEvent?): Boolean {
        return mUnityPlayer.onGenericMotionEvent(event)
    }

    lateinit var mUnityPlayer : com.unity3d.player.UnityPlayer
    override fun onUnityPlayerUnloaded() {
        moveTaskToBack(true)
    }

    override fun onUnityPlayerQuitted() {
    }
}