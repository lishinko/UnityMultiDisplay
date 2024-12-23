package com.horizon.main

import android.app.Activity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.Button
import android.widget.FrameLayout
import com.UnityTechnologies.com.unity.template.urpblank.R
import com.unity3d.player.*

class MainActivity : UnityPlayerActivity() {
    val TAG = "MainActivity"
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        val fl = findViewById<FrameLayout>(R.id.fl_content);
        fl.addView(mUnityPlayer)
        //        if (savedInstanceState == null) {
//            getSupportFragmentManager().beginTransaction()
//                    .replace(R.id.container, MainFragment.newInstance())
//                    .commitNow();
//        }
        var btn = findViewById<Button>(R.id.button)
        btn.setOnClickListener {
            Log.i(TAG, "onCreate: btn clicked")
        }
    }
}